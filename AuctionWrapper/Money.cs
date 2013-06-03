using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace AuctionWrapper
{
    /// <summary>
    /// A class to represent ingame Money, and ease the use of it. Most common operators (+,-,*,/, >,>=,==,!=) are supported with most number types and other Money objects.
    /// </summary>
    public class Money : IComparable<Money>
    {
        /// <summary>
        /// Amount of gold.
        /// </summary>
        public int Gold;
        /// <summary>
        /// Amount of silver.
        /// </summary>
        public int Silver;
        /// <summary>
        /// Amount of copper.
        /// </summary>
        public int Copper;
        /// <summary>
        /// Gets or sets the total amount of copper.
        /// </summary>
        /// <value>
        /// The total amount of copper.
        /// </value>
        public int TotalCopper
        {
            get { return Gold * 10000 + Silver * 100 + Copper; }
            set
            {
                Gold = (int)Math.Floor((double)Math.Abs(value / 10000));
                Silver = (int)Math.Floor((double)Math.Abs((value / 100) % 100));
                Copper = (int)Math.Floor((double)Math.Abs(value % 100));
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Money" /> class.
        /// </summary>
        /// <param name="totalCopper">The total amount of copper.</param>
        public Money(int totalCopper)
        {
            TotalCopper = totalCopper;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Money" /> class.
        /// </summary>
        /// <param name="gold">The amount of gold.</param>
        /// <param name="silver">The amount of silver.</param>
        /// <param name="copper">The amount ofcopper.</param>
        public Money(int gold, int silver, int copper)
        {
            Gold = gold;
            Silver = silver;
            Copper = copper;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Money" /> class.
        /// </summary>
        /// <param name="moneyRepresentation">Money representation as a string (e.g. 142g7s68c).</param>
        public Money(string moneyRepresentation)
        {
            moneyRepresentation = moneyRepresentation.ToLowerInvariant();
            var match = Regex.Match(moneyRepresentation, "([0-9]+(g|s|c))");
            if (match.Success)
            {
                var goldRegex = Regex.Match(moneyRepresentation, "([0-9]+g)");
                if (goldRegex.Success)
                {
                    Gold = int.Parse(goldRegex.Value.Replace("g", ""));
                }
                var silverRegex = Regex.Match(moneyRepresentation, "([0-9]+s)");
                if (silverRegex.Success)
                {
                    Silver = int.Parse(silverRegex.Value.Replace("s", ""));
                }
                var copperRegex = Regex.Match(moneyRepresentation, "([0-9]+c)");
                if (copperRegex.Success)
                {
                    Copper = int.Parse(copperRegex.Value.Replace("c", ""));
                }
            }
            else
            {
                int.TryParse(moneyRepresentation, out Copper);
            }
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance. Example: 2g10s5c or 5s0c if there's 0 gold.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance. Example: 2g10s5c or 5s0c if there's 0 gold.
        /// </returns>
        public override string ToString()
        {
            var moneyString = new StringBuilder();
            if (Gold > 0)
            {
                moneyString.Append(Gold);
                moneyString.Append("g");
                moneyString.Append(Silver);
                moneyString.Append("s");
                moneyString.Append(Copper);
                moneyString.Append("c");
            }
            else if (Silver > 0)
            {
                moneyString.Append(Silver);
                moneyString.Append("s");
                moneyString.Append(Copper);
                moneyString.Append("c");
            }
            else
            {
                moneyString.Append(Copper);
                moneyString.Append("c");
            }
            return moneyString.ToString();
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="copper">The total amount of copper.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(int copper)
        {
            var money = new Money(copper);
            return money.ToString();
        }
        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }
        public override int GetHashCode()
        {
            return TotalCopper;
        }
        public static Money Zero
        {
            get
            {
                return new Money(0);
            }
        }
        public int CompareTo(Money other)
        {
            if (TotalCopper < other.TotalCopper) return 1;
            if (TotalCopper > other.TotalCopper) return -1;
            return 0;
        }
        // +
        public static Money operator +(Money m1, Money m2)
        {
            return new Money(m1.TotalCopper + m2.TotalCopper);
        }
        public static Money operator +(Money m1, int number)
        {
            return m1 + new Money(number);
        }
        public static Money operator +(int number, Money m1)
        {
            return m1 + number;
        }
        public static Money operator +(Money m1, float number)
        {
            var integer = Convert.ToInt32(Math.Round(number));
            return m1 + integer;
        }
        public static Money operator +(float number, Money m1)
        {
            return m1 + number;
        }
        public static Money operator +(Money m1, double number)
        {
            var integer = Convert.ToInt32(Math.Round(number));
            return m1 + integer;
        }
        public static Money operator +(double number, Money m1)
        {
            return m1 + number;
        }
        public static Money operator +(Money m1, decimal number)
        {
            var integer = Convert.ToInt32(Math.Round(number));
            return m1 + integer;
        }
        public static Money operator +(decimal number, Money m1)
        {
            return m1 + number;
        }

        // -
        public static Money operator -(Money m1, Money m2)
        {
            return new Money(m1.TotalCopper - m2.TotalCopper);
        }
        public static Money operator -(Money m1, int number)
        {
            return m1 - new Money(number);
        }
        public static Money operator -(int number, Money m1)
        {
            return new Money(number) - m1;
        }
        public static Money operator -(Money m1, float number)
        {
            var integer = Convert.ToInt32(Math.Round(number));
            return m1 - integer;
        }
        public static Money operator -(float number, Money m1)
        {
            var integer = Convert.ToInt32(Math.Round(number));
            return integer - m1;
        }
        public static Money operator -(Money m1, double number)
        {
            var integer = Convert.ToInt32(Math.Round(number));
            return m1 - integer;
        }
        public static Money operator -(double number, Money m1)
        {
            var integer = Convert.ToInt32(Math.Round(number));
            return integer - m1;
        }
        public static Money operator -(Money m1, decimal number)
        {
            var integer = Convert.ToInt32(Math.Round(number));
            return m1 - integer;
        }
        public static Money operator -(decimal number, Money m1)
        {
            var integer = Convert.ToInt32(Math.Round(number));
            return integer - m1;
        }

        // ><
        public static bool operator >(Money m1, Money m2)
        {
            return m1.TotalCopper > m2.TotalCopper;
        }
        public static bool operator <(Money m1, Money m2)
        {
            return m1.TotalCopper < m2.TotalCopper;
        }

        public static bool operator >(Money m1, int number)
        {
            return m1.TotalCopper > number;
        }
        public static bool operator <(Money m1, int number)
        {
            return m1.TotalCopper < number;
        }

        public static bool operator >(int number, Money m1)
        {
            return number > m1.TotalCopper;
        }
        public static bool operator <(int number, Money m1)
        {
            return number < m1.TotalCopper;
        }

        public static bool operator >(Money m1, float number)
        {
            return m1.TotalCopper > number;
        }
        public static bool operator <(Money m1, float number)
        {
            return m1.TotalCopper < number;
        }

        public static bool operator >(float number, Money m1)
        {
            return number > m1.TotalCopper;
        }
        public static bool operator <(float number, Money m1)
        {
            return number < m1.TotalCopper;
        }

        public static bool operator >(Money m1, double number)
        {
            return m1.TotalCopper > number;
        }
        public static bool operator <(Money m1, double number)
        {
            return m1.TotalCopper < number;
        }

        public static bool operator >(double number, Money m1)
        {
            return number > m1.TotalCopper;
        }
        public static bool operator <(double number, Money m1)
        {
            return number < m1.TotalCopper;
        }

        public static bool operator >(Money m1, decimal number)
        {
            return m1.TotalCopper > number;
        }
        public static bool operator <(Money m1, decimal number)
        {
            return m1.TotalCopper < number;
        }
        
        public static bool operator >(decimal number, Money m1)
        {
            return number > m1.TotalCopper;
        }
        public static bool operator <(decimal number, Money m1)
        {
            return number < m1.TotalCopper;
        }

        // >= <=
        public static bool operator >=(Money m1, Money m2)
        {
            return m1.TotalCopper >= m2.TotalCopper;
        }
        public static bool operator <=(Money m1, Money m2)
        {
            return m1.TotalCopper <= m2.TotalCopper;
        }

        public static bool operator >=(Money m1, int number)
        {
            return m1.TotalCopper >= number;
        }
        public static bool operator <=(Money m1, int number)
        {
            return m1.TotalCopper <= number;
        }

        public static bool operator >=(int number, Money m1)
        {
            return number >= m1.TotalCopper;
        }
        public static bool operator <=(int number, Money m1)
        {
            return number <= m1.TotalCopper;
        }

        public static bool operator >=(Money m1, float number)
        {
            return m1.TotalCopper >= number;
        }
        public static bool operator <=(Money m1, float number)
        {
            return m1.TotalCopper <= number;
        }

        public static bool operator >=(float number, Money m1)
        {
            return number >= m1.TotalCopper;
        }
        public static bool operator <=(float number, Money m1)
        {
            return number <= m1.TotalCopper;
        }

        public static bool operator >=(Money m1, double number)
        {
            return m1.TotalCopper >= number;
        }
        public static bool operator <=(Money m1, double number)
        {
            return m1.TotalCopper <= number;
        }

        public static bool operator >=(double number, Money m1)
        {
            return number >= m1.TotalCopper;
        }
        public static bool operator <=(double number, Money m1)
        {
            return number <= m1.TotalCopper;
        }

        public static bool operator >=(Money m1, decimal number)
        {
            return m1.TotalCopper >= number;
        }
        public static bool operator <=(Money m1, decimal number)
        {
            return m1.TotalCopper <= number;
        }

        public static bool operator >=(decimal number, Money m1)
        {
            return number >= m1.TotalCopper;
        }
        public static bool operator <=(decimal number, Money m1)
        {
            return number <= m1.TotalCopper;
        }

        // *
        public static Money operator *(Money m1, Money m2)
        {
            return new Money(m1.TotalCopper * m2.TotalCopper);
        }
        public static Money operator *(Money m1, int number)
        {
            return new Money(m1.TotalCopper*number);
        }
        public static Money operator *(int number, Money m1)
        {
            return new Money(number * m1.TotalCopper);
        }
        public static Money operator *(Money m1, float number)
        {
            int newValue = Convert.ToInt32(Math.Floor(m1.TotalCopper*number));
            return new Money(newValue);
        }
        public static Money operator *(float number, Money m1)
        {
            return m1*number;
        }
        public static Money operator *(Money m1, double number)
        {
            int newValue = Convert.ToInt32(Math.Floor(m1.TotalCopper * number));
            return new Money(newValue);
        }
        public static Money operator *(double number, Money m1)
        {
            return m1 * number;
        }
        public static Money operator *(Money m1, decimal number)
        {
            int newValue = Convert.ToInt32(Math.Floor(m1.TotalCopper * number));
            return new Money(newValue);
        }
        public static Money operator *(decimal number, Money m1)
        {
            return m1 * number;
        }

        // /
        public static Money operator /(Money m1, Money m2)
        {
            return new Money(m1.TotalCopper / m2.TotalCopper);
        }
        public static Money operator /(Money m1, int number)
        {
            return new Money(m1.TotalCopper / number);
        }
        public static Money operator /(int number, Money m1)
        {
            return new Money(number / m1.TotalCopper);
        }
        public static Money operator /(Money m1, float number)
        {
            int newValue = Convert.ToInt32(Math.Floor(m1.TotalCopper / number));
            return new Money(newValue);
        }
        public static Money operator /(float number, Money m1)
        {
            int newValue = Convert.ToInt32(Math.Floor(number / m1.TotalCopper));
            return new Money(newValue);
        }
        public static Money operator /(Money m1, double number)
        {
            int newValue = Convert.ToInt32(Math.Floor(m1.TotalCopper * number));
            return new Money(newValue);
        }
        public static Money operator /(double number, Money m1)
        {
            int newValue = Convert.ToInt32(Math.Floor(number / m1.TotalCopper));
            return new Money(newValue);
        }
        public static Money operator /(Money m1, decimal number)
        {
            int newValue = Convert.ToInt32(Math.Floor(m1.TotalCopper / number));
            return new Money(newValue);
        }
        public static Money operator /(decimal number, Money m1)
        {
            int newValue = Convert.ToInt32(Math.Floor(number / m1.TotalCopper));
            return new Money(newValue);
        }

        public static bool operator ==(Money m1, Money m2)
        {
            Debug.Assert(m1 != null, "m1 != null");
            Debug.Assert(m2 != null, "m2 != null");
            return m1.TotalCopper == m2.TotalCopper;
        }

        public static bool operator !=(Money m1, Money m2)
        {
            Debug.Assert(m1 != null, "m1 != null");
            Debug.Assert(m2 != null, "m2 != null");
            return m1.TotalCopper != m2.TotalCopper;
        }
        public static bool operator ==(Money m1, int number)
        {
            Debug.Assert(m1 != null, "m1 != null");
            return m1.TotalCopper == number;
        }
        public static bool operator !=(Money m1, int number)
        {
            Debug.Assert(m1 != null, "m1 != null");
            return m1.TotalCopper != number;
        }
        public static bool operator ==(int number, Money m1)
        {
            return m1 == number;
        }
        public static bool operator !=(int number, Money m1)
        {
            return m1 != number;
        }
    }
}
