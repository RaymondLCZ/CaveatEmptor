using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zee.Sample.CaveatEmptor.Model
{
    /// <summary> 
    /// A single item in a single category, with additional information.
    /// This is really a very special mapping. The CategorizedItem class
    /// represents an association table. The ER model for this is really
    /// a many-to-many association, but instead of two entities and two
    /// collections, we mapped this as two one-to-many associations between
    /// three entities. One of the motivation for this are the additional
    /// attributes on the association table (not only two FKs): username
    /// and creation date.
    /// </summary>
    /// <remarks>
    /// This is really a very special mapping. The CategorizedItem class
    /// represents an association table. The ER model for this is really
    /// a many-to-many association, but instead of two entities and two
    /// collections, we mapped this as two one-to-many associations between
    /// three entities. One of the motivation for this are the additional
    /// attributes on the association table (not only two FKs): username
    /// and creation date.
    ///
    /// This class is the entity in the middle, between Category and Item.
    /// You can see that it has references to both. The trick is the usage
    /// of update="false" insert="false" on the <many-to-one> mapping
    /// elements. The foreign/primary key columns of the association table
    /// is therefore managed by the <key-property> mappings in the composite
    /// key.
    ///
    /// Note that the composite key is encapsulated in an inner class, which
    /// simplifies the implementation of equals/hashCode. We recommend to
    /// always use a separate composite key class.
    /// </remarks>
    /// <seealso cref="Category">
    /// </seealso>
    /// <seealso cref="Item">
    /// </seealso>
    [Serializable]
    public class CategorizedItem : IComparable
    {
        private readonly Category category;
        private readonly DateTime dateAdded = SystemTime.NowWithoutMilliseconds;
        private readonly CategorizedItemId id = new CategorizedItemId();
        private readonly Item item;
        private readonly string username; // This could also be an association to User

        #region constructor
        /// <summary> 
        /// No-arg constructor for tools.
        /// </summary>
        internal CategorizedItem()
        {
        }

        /// <summary>
        /// Full constructor
        /// </summary>
        public CategorizedItem(string username, Category category, Item item)
        {
            this.username = username;

            this.category = category;
            this.item = item;

            // Set key values
            id = new CategorizedItemId(category.Id, item.Id);

            // Guarantee referential integrity
            category.CategorizedItems.Add(this);
            item.CategorizedItems.Add(this);
        }
        #endregion

        #region Common Methods

        public virtual int CompareTo(object o)
        {
            // CategorizedItems are sorted by date
            if (o is CategorizedItem)
                return DateAdded.CompareTo(((CategorizedItem)o).DateAdded);
            return 0;
        }


        public override string ToString()
        {
            return "Added by: '" + Username + "', " + "On Date: '" + DateAdded.ToString("r");
        }

        #endregion

        //[CompositeId(1, Name = "Id", Access = "nosetter.camelcase", UnsavedValue = UnsavedValueType.Any)]
        //[KeyProperty(2, Name = "categoryId", Access = "field")]
        //[KeyProperty(3, Name = "itemId", Access = "field")]
        public virtual CategorizedItemId Id
        {
            get { return id; }
        }

        //[Property(Column = "USERNAME", Update = false, NotNull = true, Access = "nosetter.camelcase")]
        public virtual string Username
        {
            get { return username; }
        }

        //[Property(Column = "DATE_ADDED", Update = false, NotNull = true, Access = "nosetter.camelcase")]
        public virtual DateTime DateAdded
        {
            get { return dateAdded; }
        }

        //[ManyToOne(Column = "CATEGORY_ID", Update = false, NotNull = true, Access = "nosetter.camelcase")]
        public virtual Category Category
        {
            get { return category; }
        }

        //[ManyToOne(Column = "ITEM_ID", Update = false, NotNull = true, Access = "nosetter.camelcase")]
        public virtual Item Item
        {
            get { return item; }
        }
    }
}
