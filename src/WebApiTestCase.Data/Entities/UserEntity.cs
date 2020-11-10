using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiTestCase.Core.Enums;
using WebApiTestCase.Data.Entities.Common;

namespace WebApiTestCase.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Column(TypeName = "int")]
        public UserStatus Status { get; set; }

        public ICollection<TaskEntity> ProviderTasks { get; set; }

        public ICollection<TaskEntity> PerformerTasks { get; set; }
    }
}