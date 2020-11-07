using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiTestCase.Core.Enums;
using WebApiTestCase.Data.Entities.Common;

namespace WebApiTestCase.Data.Entities
{
    public class TaskEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "int")]
        public TaskStatus Status { get; set; }

        public int PerformerId { get; set; }

        [Required]
        public UserEntity Performer { get; set; }

        public int ProviderId { get; set; }

        [Required]
        public UserEntity Provider { get; set; }
    }
}