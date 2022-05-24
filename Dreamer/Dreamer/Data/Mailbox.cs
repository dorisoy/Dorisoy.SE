using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dreamer.Data
{
    public class Mailbox
    {
        [Key]
        public long MailboxId { get; set; }
        [Display(Name = "To")]
        [Required]
        [EmailAddress(ErrorMessage = "Enter a proper email address")]
        public string ToEmail { get; set; }
        [Display(Name = "Customer")]
        [Required]
        public long LedgerId { get; set; }
        [Display(Name = "Subject")]
        [Required]
        public string Subject { get; set; }
        [Display(Name = "Body")]
        [Required]
        public string Body { get; set; }
        public string Attachment { get; set; }
        [Display(Name = "Sender Email")]
        [Required]
        [EmailAddress(ErrorMessage = "Enter a proper email address")]
        public string Email { get; set; }
        [Display(Name = "Sender Password")]
        [Required]
        public string Password { get; set; }
        [Display(Name = "File Name")]
        public string FileName { get; set; }
        public long CompanyId { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
