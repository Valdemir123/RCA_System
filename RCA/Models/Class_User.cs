using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace RCA.Models
{
    public enum UserStatus : int
    {
        Ativo = 1,
        Suspenso = 2
    }
    public enum UserTypeAccess : int
    {
        Master = 1,
        Admin = 2,
        User =3
    }

    [Table("User")]
    public class Class_User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Status")]
        public UserStatus StatusId { get; set; }

        [ForeignKey("Company")]
        [DisplayName("Companhia")]
        public int CompanyId { get; set; }

        [DisplayName("Perfil Acesso")]
        public UserTypeAccess TypeAccessId { get; set; }

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(50)]
        [DisplayName("Nome Completo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(50)]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0} Formato Obrigatório, user@provedor.com.br")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(20)]
        [DisplayName("Celular")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "{0} Preenchimento Obrigatório")]
        [StringLength(20)]
        [DisplayName("UserLogin")]
        public string UserName { get; set; }

        [StringLength(100)]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class Class_UserLIST
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public List<Class_UserITEM> UserLIST { get; set; } = new List<Class_UserITEM>();
    }
    public class Class_UserITEM
    {
        public int Id { get; set; }

        [DisplayName("Status")]
        public UserStatus StatusId { get; set; }

        [DisplayName("UserName")]
        public string UserName { get; set; }

        [DisplayName("Nome Completo")]
        public string Name { get; set; }

        [DisplayName("Perfil Acesso")]
        public UserTypeAccess TypeAccessId { get; set; }

    }
}
