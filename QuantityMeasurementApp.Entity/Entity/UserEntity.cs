using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantityAppModel
{
    [Table("Users")]
    public class UserEntity
    {
        public int Id{get; set;}

        [EmailAddress]
        public string Email{get; set;}
        public string PasswordHash{ get; set; }
        public string Salt { get; set;}
        public DateTime CreatedAt { get; set;}
    }
}