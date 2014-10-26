using System;

namespace Zee.Sample.CaveatEmptor.Model
{
    /// <summary> 
    /// This billing strategy can handle various credit cards.
    /// The type of credit card is handled with a typesafe
    /// enumeration, CreditCardType.
    /// </summary>
    /// <seealso cref="CreditCardType">
    /// </seealso>
    [Serializable]
    // CreditCard subclass mapping to its own table, normalized. CreditCard is immutable, we map all properties with update="false"
    public class CreditCard : BillingDetails
    {
        private readonly string expMonth;
        private readonly string expYear;
        private readonly string number;
        private readonly CreditCardType type;

        /// <summary> No-arg constructor for tools.</summary>
        internal CreditCard()
        {
        }

        /// <summary> Full constructor. </summary>
        public CreditCard(string ownerName, User user, string number, CreditCardType type, string expMonth,
                          string expYear) : base(ownerName, user)
        {
            this.type = type;
            this.number = number;
            this.expMonth = expMonth;
            this.expYear = expYear;
        }

        #region Common

        public override string ToString()
        {
            return "CreditCard ('" + Id + "'), " + "Type: '" + Type + "'";
        }

        public override bool Equals(object o)
        {
            if (this == o)
                return true;
            if (!(o is CreditCard))
                return false;

            var billingDetails = (BillingDetails) o;

            if (Created != billingDetails.Created)
                return false;
            if (OwnerName != billingDetails.OwnerName)
                return false;

            return true;
        }


        public override int GetHashCode()
        {
            int result;
            result = Created.GetHashCode();
            result = 29*result + OwnerName.GetHashCode();
            return result;
        }

        #endregion

        public virtual CreditCardType Type
        {
            get { return type; }
        }

        public virtual string Number
        {
            get { return number; }
        }

        public virtual string ExpMonth
        {
            get { return expMonth; }
        }

        public virtual string ExpYear
        {
            get { return expYear; }
        }

        public override bool Valid
        {
            get
            {
                // Use the type to validate the CreditCard details.
                // Implement your own syntactical validation of credit card information.
                return true; // Type.IsValid(this);
            }
        }
    }
}