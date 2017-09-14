using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseEntity
{
    public class EBase
    {
        private string mCreatedBy { get; set; }
        private string mUpdatedBy { get; set; }
        private DateTime? mCreatedDate { get; set; }
        private DateTime? mUpdatedDate { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedDate
        {
            get
            {
                if (mCreatedDate.HasValue)
                {
                    return mCreatedDate;
                }
                else
                {
                    return DateTime.Now;
                }
            }
            set
            {
                mCreatedDate = value;
            }
        }

        public int CreatedBy
        {
            get
            {
                return Convert.ToInt32(mCreatedBy);
            }
            set
            {
                mCreatedBy = value.ToString();
            }
        }

        [Column(TypeName = "DateTime2")]
        public DateTime? UpdatedDate
        {
            get
            {
                if (mUpdatedDate.HasValue)
                {
                    return mUpdatedDate;
                }
                else
                {
                    return DateTime.Now;
                }
            }
            set
            {
                mUpdatedDate = value;
            }
        }

        public int UpdatedBy
        {
            get
            {
                return Convert.ToInt32(mUpdatedBy);
            }
            set
            {
                mUpdatedBy = value.ToString();
            }
        }
    }
}
