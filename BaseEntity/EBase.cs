using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseEntity
{
    public abstract class EBase
    {
        private DateTime? _createdDate { get; set; }
        private DateTime? _updatedDate { get; set; }

        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime? UpdatedDate { get; set; }
    }
}
