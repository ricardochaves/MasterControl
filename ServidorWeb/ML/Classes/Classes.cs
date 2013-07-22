using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServidorWeb.ML.Classes
{


    public class Identification
    {
        public string type { get; set; }
        public string number { get; set; }
    }

    public class Phone
    {
        public string area_code { get; set; }
        public string number { get; set; }
        public string extension { get; set; }
        public bool verified { get; set; }
    }

    public class Ratings
    {
        public double positive { get; set; }
        public double negative { get; set; }
        public double neutral { get; set; }
    }

    public class TransactionsSeller
    {
        public string period { get; set; }
        public int total { get; set; }
        public int completed { get; set; }
        public int canceled { get; set; }
        public Ratings ratings { get; set; }
    }

    public class SellerReputation
    {
        public string level_id { get; set; }
        public string power_seller_status { get; set; }
        public TransactionsSeller transactions { get; set; }
    }
    
    public class TransactionState
    {
        public int total { get; set; }
        public int paid { get; set; }
        public int units { get; set; }
    }

    public class TransactionsBuyer
    {
        public string period { get; set; }
        public int total { get; set; }
        public int completed { get; set; }
        public TransactionState canceled { get; set; }
        public TransactionState unrated { get; set; }
        public TransactionState not_yet_rated { get; set; }
    }

    public class BuyerReputation
    {
        public int canceled_transactions { get; set; }
        public TransactionsBuyer transactions { get; set; }
    }

    public class ImmediatePayment
    {
        public bool required { get; set; }
        public List<object> reasons { get; set; }
    }

    public class List
    {
        public bool allow { get; set; }
        public List<object> codes { get; set; }
        public ImmediatePayment immediate_payment { get; set; }
    }

    public class Status
    {
        public string site_status { get; set; }
        public List list { get; set; }
        public List buy { get; set; }
        public List sell { get; set; }
        public List billing { get; set; }
        public bool mercadopago_tc_accepted { get; set; }
        public string mercadopago_account_type { get; set; }
        public string mercadoenvios { get; set; }
        public bool immediate_payment { get; set; }
    }

    public class Credit
    {
        public double consumed { get; set; }
        public string credit_level_id { get; set; }
    }

    public class Usuario
    {
        //public int id { get; set; }
        //public string nickname { get; set; }
        //public DateTime registration_date { get; set; }
        //public string first_name { get; set; }
        //public string last_name { get; set; }
        //public string country_id { get; set; }
        //public string email { get; set; }
        //public Identification identification { get; set; }
        //public Phone phone { get; set; }
        //public Phone alternative_phone { get; set; }
        //public string user_type { get; set; }
        //public List<string> tags { get; set; }
        //public string logo { get; set; }
        //public int points { get; set; }
        //public string site_id { get; set; }
        //public string permalink { get; set; }
        //public List<string> shipping_modes { get; set; }
        //public string seller_experience { get; set; }
        //public SellerReputation seller_reputation { get; set; }
        //public BuyerReputation buyer_reputation { get; set; }
        //public Status status { get; set; }
        //public Credit credit { get; set; }
        //public BillingInfo billing_info { get; set; }


        public int id { get; set; }
        public string nickname { get; set; }
        public string registration_date { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string country_id { get; set; }
        public string email { get; set; }
        public Identification identification { get; set; }
        public Address address { get; set; }
        public Phone phone { get; set; }
        public Phone alternative_phone { get; set; }
        public string user_type { get; set; }
        public List<string> tags { get; set; }
        public string logo { get; set; }
        public int points { get; set; }
        public string site_id { get; set; }
        public string permalink { get; set; }
        public List<string> shipping_modes { get; set; }
        public string seller_experience { get; set; }
        //public SellerReputation seller_reputation { get; set; }
        //public BuyerReputation buyer_reputation { get; set; }
        public Status status { get; set; }
        public Credit credit { get; set; }


    }

    public class Address
    {
        public string state { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string zip_code { get; set; }
    }



    public class Paging
    {
        public int total { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string title { get; set; }
        public string variation_id { get; set; }
        public List<object> variation_attributes { get; set; }
    }

    public class OrderItem
    {
        public Item item { get; set; }
        public int quantity { get; set; }
        public double unit_price { get; set; }
        public string currency_id { get; set; }
    }

    public class BillingInfo
    {
        public string doc_type { get; set; }
        public string doc_number { get; set; }
    }

    public class Phone2
    {
        public string area_code { get; set; }
        public string number { get; set; }
        public string extension { get; set; }
    }

    public class Sale
    {
        public DateTime date_created { get; set; }
        public bool fulfilled { get; set; }
        public string rating { get; set; }
    }

    public class Purchase
    {
        public DateTime date_created { get; set; }
        public bool fulfilled { get; set; }
        public string rating { get; set; }
    }

    public class Feedback
    {
        public Sale sale { get; set; }
        public Purchase purchase { get; set; }
    }

    public class City
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class State
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Country
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ReceiverAddress
    {
        public object id { get; set; }
        public string address_line { get; set; }
        public string zip_code { get; set; }
        public string comment { get; set; }
        public City city { get; set; }
        public State state { get; set; }
        public Country country { get; set; }
        public object latitude { get; set; }
        public object longitude { get; set; }
    }

    public class Shipping
    {
        public string status { get; set; }
        public long? id { get; set; }
        public string shipment_type { get; set; }
        public string date_created { get; set; }
        public string currency_id { get; set; }
        public double? cost { get; set; }
        public ReceiverAddress receiver_address { get; set; }
    }

    public class Order
    {
        public int id { get; set; }
        public string status { get; set; }
        public StatusDetail status_detail { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_closed { get; set; }
        public List<OrderItem> order_items { get; set; }
        public double total_amount { get; set; }
        public string currency_id { get; set; }
        public Usuario buyer { get; set; }
        public Usuario seller { get; set; }
        public List<Payment> payments { get; set; }
        public Feedback feedback { get; set; }
        public Shipping shipping { get; set; }
        public List<string> tags { get; set; }
    }

    public class Sort
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class AvailableSort
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public string name { get; set; }
        public int results { get; set; }
    }

    public class AvailableFilter
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public List<Value> values { get; set; }
    }

    public class ListOrder
    {
        public string query { get; set; }
        public string display { get; set; }
        public Paging paging { get; set; }
        public List<Order> results { get; set; }
        public Sort sort { get; set; }
        public List<AvailableSort> available_sorts { get; set; }
        public List<object> filters { get; set; }
        public List<AvailableFilter> available_filters { get; set; }
        public object complete { get; set; }
    }

    public class Payment
    {
        public int id { get; set; }
        public string status { get; set; }
        public double transaction_amount { get; set; }
        public string currency_id { get; set; }
        public DateTime date_created { get; set; }
        public string date_last_updated { get; set; }
    }

    public class StatusDetail
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class CallBackTemp
    {
        public string user_id { get; set; }
        public string resource { get; set; }
        public string topic { get; set; }
        public DateTime received { get; set; }
        public long application_id { get; set; }
        public DateTime sent { get; set; }
        public int attempts { get; set; }
    }

    //PERGUNTAS DO ML
    public class Answer
    {
        public DateTime date_created { get; set; }
        public string status { get; set; }
        public string text { get; set; }
    }

    public class From
    {
        public int id { get; set; }
        public int answered_questions { get; set; }
    }

    public class Question
    {
        public long id { get; set; }
        public Answer answer { get; set; }
        public DateTime date_created { get; set; }
        public string item_id { get; set; }
        public int seller_id { get; set; }
        public string status { get; set; }
        public string text { get; set; }
        public From from { get; set; }
    }

}
