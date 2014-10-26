using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zee.Sample.CaveatEmptor.Model
{
    /// <summary>
    /// Composite ID class for Categorized Item
    /// </summary>
    [Serializable]
    public class CategorizedItemId
    {
        private readonly long categoryId;
        private readonly long itemId;

        public CategorizedItemId()
        {
        }

        public CategorizedItemId(long categoryId, long itemId)
        {
            this.categoryId = categoryId;
            this.itemId = itemId;
        }

        #region Common Methods

        public override bool Equals(object o)
        {
            if (o is CategorizedItemId)
            {
                var that = (CategorizedItemId)o;
                return categoryId == that.categoryId && itemId == that.itemId;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return categoryId.GetHashCode() + itemId.GetHashCode();
        }

        #endregion
    }
}
