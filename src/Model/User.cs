using System;
using System.Linq;
using System.Text;
using Iesi.Collections;
using Iesi.Collections.Generic;
using Zee.Sample.CaveatEmptor.Model.Exceptions;

/*
A User is a versioned entity, with some special properties.
One is the username, it is immutable and unique. The
defaultBillingDetails property points to one of the
BillingDetails in the collection of all BillingDetails.

We never load any BillingDetails when a User is retrieved.
*/

namespace Zee.Sample.CaveatEmptor.Model
{
    /// <summary> A user of the CaveatEmptor auction application.
    /// 
    /// This class represents the user entity of CaveatEmptor business.
    /// The associations are: a Set of Items the user
    /// is selling, a Set of Bids the user has made,
    /// and an Address component. Also a Set of
    /// BuyNows, that is, immediate buys made for an item.
    /// 
    /// The BillingDetails are used to calculate and bill the
    /// user for his activities on our system. The username
    /// and password are used as login credentials. The
    /// ranking is a number that is increased by each successful
    /// transaction, but may also be manually increased (or decreased) by
    /// the system administrators.
    /// </summary>
    [Serializable]
    public class User : IComparable
    {
        #region member field
        private readonly ISet<BillingDetails> billingDetails = new HashedSet<BillingDetails>();
        private readonly DateTime created = SystemTime.NowWithoutMilliseconds;
        private readonly ISet<Item> items = new HashedSet<Item>();
        private readonly string username;
        private string email;
        private string firstname;
        private Address homeAddress;
        private long id;
        private string lastname;
        private string password;
        private int version;
        #endregion

        #region constructor
        /// <summary> No-arg constructor for tools.</summary>
        protected User()        {        }

        /// <summary> Full constructor.</summary>
        public User(string firstname, string lastname, string username, string password, string email, Address address,
                    ISet<Item> items, ISet<BillingDetails> billingDetails)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.username = username;
            this.password = password;
            this.email = email;
            homeAddress = address;
            this.items = items;
            this.billingDetails = billingDetails;
        }

        /// <summary> Simple constructor.</summary>
        public User(string firstname, string lastname, string username, string password, string email)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.username = username;
            this.password = password;
            this.email = email;
        }
        #endregion

        #region property
        public virtual long Id
        {
            get { return id; }
        }

        public virtual int Version
        {
            get { return version; }
        }

        public virtual string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public virtual string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public virtual string Username
        {
            get { return username; }
        }

        public virtual string Password
        {
            get { return password; }
            set { password = value; }
        }

        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }

        public virtual int Ranking { get; set; }
        
        public virtual Address HomeAddress
        {
            get { return homeAddress; }
            set { homeAddress = value; }
        }


        /// <summary>
        /// This is mapped by the BillingAddressComponent below
        /// </summary>
        public virtual Address BillingAddress { get; set; }

        //The default billing strategy, may be null if no BillingDetails exist.
        public virtual BillingDetails DefaultBillingDetails { get; set; }

        public virtual DateTime Created
        {
            get { return created; }
        }

        public virtual bool IsAdmin { get; set; }

        public virtual ISet<Item> Items
        {
            get { return items; }
        }

        public virtual ISet<BillingDetails> BillingDetails
        {
            get { return billingDetails; }
        }

        #endregion

        public virtual void AddItem(Item item)
        {
            if (item == null)
                throw new ArgumentException("Can't add a null Item.");
            Items.Add(item);
        }

        /// <summary> Adds a BillingDetails to the set.
        /// 
        /// This method checks if there is only one billing method
        /// in the set, then makes this the default.
        /// 
        /// </summary>
        public virtual void AddBillingDetails(BillingDetails billingDetails)
        {
            if (billingDetails == null)
                throw new ArgumentException("Can't add a null BillingDetails.");

            bool added = BillingDetails.Add(billingDetails);
            if (!added)
                throw new ArgumentException("Duplicates not allowed");

            if (BillingDetails.Count == 1)
            {
                DefaultBillingDetails = billingDetails;
            }
        }

        /// <summary> Removes a BillingDetails from the set.
        /// 
        /// This method checks if the removed is the default element,
        /// and will throw a BusinessException if there is more than one
        /// left to chose from. This might actually not be the best way
        /// to handle this situation.
        /// 
        /// </summary>
        public virtual void RemoveBillingDetails(BillingDetails billingDetails)
        {
            if (billingDetails == null)
                throw new ArgumentException("Can't remove a null BillingDetails.");

            if (BillingDetails.Count >= 2)
            {
                BillingDetails.Remove(billingDetails);
                DefaultBillingDetails = BillingDetails.GetEnumerator().Current;
            }
            else
            {
                throw new BusinessException("Please set new default BillingDetails first");
            }
        }

        #region Common Methods

        public virtual int CompareTo(object o)
        {
            if (o is User)
                return Created.CompareTo(((User)o).Created);
            return 0;
        }

        public override bool Equals(object o)
        {
            if (this == o)
                return true;

            if (!(o is User))
                return false;

            var user = (User)o;

            if (username != user.Username)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return username.GetHashCode();
        }


        public override string ToString()
        {
            return "User ('" + Id + "'), " + "Username: '" + Username + "'";
        }

        #endregion

        #region Business Methods

        public virtual void IncreaseRanking()
        {
            Ranking = Ranking + 1;
        }

        #endregion

        #region Nested type: BillingAddressComponent

        /// <summary>
        /// These class allow use of Mapping Attributes to work 
        /// with components. Not that this isn't a very elegant solution.
        /// </summary>
        private class BillingAddressComponent : Address
        {
            private string ingoreMe;
        }

        #endregion
    }
}
