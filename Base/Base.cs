using System;

namespace Base
{
    [Obsolete("Use BaseModel project instead")]
    public class Base
    {
        public Base()
        {
            Id = 0;
        }

        private string mCreatedBy { get; set; }
        private string mUpdatedBy { get; set; }

        public int Id { get; set; }

        public virtual int CreatedBy
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

        public virtual int UpdatedBy
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
