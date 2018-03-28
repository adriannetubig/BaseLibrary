using System;

namespace BaseModel
{
    public abstract class Base
    {
        private DateTime? _createdDate { get; set; }
        private DateTime? _updatedDate { get; set; }

        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public DateTime? CreatedDate
        {
            get
            {
                if (_createdDate.HasValue)
                {
                    return _createdDate;
                }
                else
                {
                    return DateTime.Now;
                }
            }
            set
            {
                _createdDate = value;
            }
        }
        public DateTime? UpdatedDate
        {
            get
            {
                if (_updatedDate.HasValue)
                {
                    return _updatedDate;
                }
                else
                {
                    return DateTime.Now;
                }
            }
            set
            {
                _updatedDate = value;
            }
        }
    }
}
