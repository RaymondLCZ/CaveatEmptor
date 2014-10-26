using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zee.Sample.CaveatEmptor.Model
{
    public class Bid : IComparable
    {
        #region member field
        // Common id property.
        private readonly MonetaryAmount amount;
        private readonly User bidder;
        private readonly Item item;
        private DateTime created = SystemTime.NowWithoutMilliseconds;
        private long id;
        private int version;
        #endregion

        /// <summary> No-arg constructor for tools.</summary>
        protected Bid()
        {
        }

        /// <summary> Full constructor. </summary>
        public Bid(MonetaryAmount amount, Item item, User bidder)
        {
            this.amount = amount;
            this.item = item;
            this.bidder = bidder;
        }

        #region Common Methods

        public virtual int CompareTo(object o)
        {
            if (o is Bid)
            {
                return Created.CompareTo(((Bid)o).Created);
            }
            return 0;
        }

        public override bool Equals(object o)
        {
            if (this == o)
                return true;

            if (!(o is Bid))
                return false;

            var bid = (Bid)o;

            //TODO: Why isn't != working in Bid Equals()
            //Below, != doesn't work (I'm missing something I know)
            if (!Amount.Equals(bid.Amount))
                return false;

            if (Created != bid.Created)
                return false;

            return true;
        }


        public override int GetHashCode()
        {
            int result;
            result = amount.GetHashCode();
            result = 29 * result + created.GetHashCode();
            return result;
        }


        public override string ToString()
        {
            return "Bid ('" + Id + "'), " + "Created: '" + Created.ToString("r") + "' " + "Amount: '" + Amount + "'";
        }

        #endregion

        public virtual long Id
        {
            get { return id; }
        }

        public virtual int Version
        {
            get { return version; }
        }

        /// <remarks>
        /// UserType for prices, length is precision of decimal field for DDL.
        /// </remarks>
        public virtual MonetaryAmount Amount
        {
            get { return amount; }
        }

        /// <remarks>
        /// The other side of this bidirectional one-to-many association to item.
        /// </remarks>
        public virtual Item Item
        {
            get { return item; }
        }

        /// <remarks> 
        /// The other side of this bidirectional one-to-many association to user.
        /// </remarks>
        public virtual User Bidder
        {
            get { return bidder; }
        }

        /// <remarks>
        /// We can't change the creation time, so map it with update="false".
        /// </remarks>
        public virtual DateTime Created
        {
            get { return created; }
        }
    }
}
