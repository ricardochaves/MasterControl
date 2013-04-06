﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServidorWeb.ML.Classes
{
    public class MP
    {
        public static readonly String version = "0.1.5";

        private readonly String client_id;
        private readonly String client_secret;
        private JObject access_data = null;

        public MP(String client_id, String client_secret)
        {
            this.client_id = client_id;
            this.client_secret = client_secret;
        }

        /**
         * Get Access Token for API use
         */
        public String getAccessToken()
        {
            Dictionary<String, String> appClientValues = new Dictionary<String, String>();
            appClientValues.Add("grant_type", "client_credentials");
            appClientValues.Add("client_id", this.client_id);
            appClientValues.Add("client_secret", this.client_secret);

            String appClientValuesQuery = this.buildQuery(appClientValues);

            JObject access_data = RestClient.post("/oauth/token", appClientValuesQuery, RestClient.MIME_FORM);

            if (((int)access_data.SelectToken("status")) == 200)
            {
                this.access_data = (JObject)access_data.SelectToken("response");
                return (String)this.access_data["access_token"];
            }
            else
            {
                throw new Exception(JsonConvert.SerializeObject(access_data));
            }
        }

        /**
         * Get information for specific payment
         * @param id
         * @return
         */
        public JObject getPaymentInfo(String id)
        {
            String accessToken;
            try
            {
                accessToken = this.getAccessToken();
            }
            catch (Exception e)
            {
                return JObject.Parse(e.Message);
            }

            JObject paymentInfo = RestClient.get("/collections/notifications/" + id + "?access_token=" + accessToken);

            return paymentInfo;
        }

        /**
         * Refund accredited payment
         * @param id
         * @return
         */
        public JObject refundPayment(String id)
        {
            String accessToken;
            try
            {
                accessToken = this.getAccessToken();
            }
            catch (Exception e)
            {
                return JObject.Parse(e.Message);
            }

            JObject refundStatus = new JObject(
                        new JProperty("status", "refunded")
                    );

            JObject response = RestClient.put("/collections/" + id + "?access_token=" + accessToken, refundStatus);

            return response;
        }

        /**
         * Cancel pending payment
         * @param id
         * @return
         */
        public JObject cancelPayment(String id)
        {
            String accessToken;
            try
            {
                accessToken = this.getAccessToken();
            }
            catch (Exception e)
            {
                return JObject.Parse(e.Message);
            }

            JObject cancelStatus = new JObject(
                        new JProperty("status", "cancelled")
                    );

            JObject response = RestClient.put("/collections/" + id + "?access_token=" + accessToken, cancelStatus);

            return response;
        }

        /**
         * Search payments according to filters, with pagination
         * @param filters
         * @param offset
         * @param limit
         * @return
         */
        public JObject searchPayment(Dictionary<String, String> filters, long offset = 0, long limit = 0)
        {
            String accessToken;
            try
            {
                accessToken = this.getAccessToken();
            }
            catch (Exception e)
            {
                return JObject.Parse(e.Message);
            }

            filters.Add("offset", offset.ToString());
            filters.Add("limit", limit.ToString());

            String filtersQuery = this.buildQuery(filters);

            JObject collectionResult = RestClient.get("/collections/search?" + filtersQuery + "&access_token=" + accessToken);
            return collectionResult;
        }

        /**
         * Create a checkout preference
         * @param preference
         * @return
         */
        public JObject createPreference(String preference)
        {
            JObject preferenceJSON = JObject.Parse(preference);
            return this.createPreference(preferenceJSON);
        }
        public JObject createPreference(JObject preference)
        {
            String accessToken;
            try
            {
                accessToken = this.getAccessToken();
            }
            catch (Exception e)
            {
                return JObject.Parse(e.Message);
            }

            JObject preferenceResult = RestClient.post("/checkout/preferences?access_token=" + accessToken, preference);
            return preferenceResult;
        }

        /**
         * Update a checkout preference
         * @param string $id
         * @param array $preference
         * @return
         */
        public JObject updatePreference(String id, String preference)
        {
            JObject preferenceJSON = JObject.Parse(preference);
            return this.updatePreference(id, preferenceJSON);
        }
        public JObject updatePreference(String id, JObject preference)
        {
            String accessToken;
            try
            {
                accessToken = this.getAccessToken();
            }
            catch (Exception e)
            {
                return JObject.Parse(e.Message);
            }

            JObject preferenceResult = RestClient.put("/checkout/preferences/" + id + "?access_token=" + accessToken, preference);
            return preferenceResult;
        }

        /**
         * Get a checkout preference
         * @param id
         * @return
         */
        public JObject getPreference(String id)
        {
            String accessToken;
            try
            {
                accessToken = this.getAccessToken();
            }
            catch (Exception e)
            {
                return JObject.Parse(e.Message);
            }

            JObject preferenceResult = RestClient.get("/checkout/preferences/" + id + "?access_token=" + accessToken);
            return preferenceResult;
        }

        /*****************************************************************************************************/
        private String buildQuery<T>(Dictionary<String, T> parameters)
        {
            String[] query = new String[parameters.Count];
            int index = 0;

            var enumerator = parameters.GetEnumerator();
            while (enumerator.MoveNext())
            {
                String val = enumerator.Current.Value != null ? enumerator.Current.Value.ToString() : "";
                val = HttpUtility.UrlEncode(val);
                query[index++] = enumerator.Current.Key + "=" + val;
            }

            return String.Join("&", query);
        }

        private static class Util
        {
            public static T get<K, T>(Dictionary<K, T> dict, K key, T def)
            {
                return dict.ContainsKey(key) ? dict[key] : def;
            }
        }

        private static class RestClient
        {
            private const String API_BASE_URL = "https://api.mercadolibre.com";
            public const String MIME_JSON = "application/json";
            public const String MIME_FORM = "application/x-www-form-urlencoded";

            private static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            }

            private static JObject exec(String method, String uri, Object data, String contentType)
            {
                JObject response;

                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(API_BASE_URL + uri);
                request.UserAgent = "MercadoPago .NET SDK v" + MP.version;
                request.Accept = MIME_JSON;
                request.Method = method;
                request.ContentType = contentType;
                setData(request, data, contentType);

                String responseBody = null;
                try
                {
                    HttpWebResponse apiResult = (HttpWebResponse)request.GetResponse();
                    responseBody = new StreamReader(apiResult.GetResponseStream()).ReadToEnd();
                    response = new JObject(
                        new JProperty("status", (int)apiResult.StatusCode),
                        new JProperty("response", JObject.Parse(responseBody))
                    );
                }
                catch (WebException e)
                {
                    responseBody = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                    try
                    {
                        response = new JObject(
                            new JProperty("status", (int)((HttpWebResponse)e.Response).StatusCode),
                            new JProperty("response", JObject.Parse(responseBody))
                        );
                    }
                    catch (JsonReaderException je)
                    {
                        response = new JObject(
                            new JProperty("status", 500),
                            new JProperty("response", je.Message)
                        );
                    }
                }

                return response;
            }

            private static void setData(HttpWebRequest request, Object data, String contentType)
            {
                if (data != null)
                {
                    String dataString = data.ToString();
                    if (dataString.Length > 0)
                    {
                        using (Stream requestStream = request.GetRequestStream())
                        {
                            using (StreamWriter writer = new StreamWriter(requestStream))
                            {
                                writer.Write(dataString);
                            }
                        }
                    }
                }
            }

            public static JObject get(String uri, String contentType = MIME_JSON)
            {
                return exec("GET", uri, null, contentType);
            }

            public static JObject post(String uri, Object data, String contentType = MIME_JSON)
            {
                return exec("POST", uri, data, contentType);
            }

            public static JObject put(String uri, Object data, String contentType = MIME_JSON)
            {
                return exec("PUT", uri, data, contentType);
            }
        }

    }
}