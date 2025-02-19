using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class ClaimsDetail
    {
        [Key]
        public int ClaimId { get; set; }
        public int TypeId { get; set; }
        public int FundId { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public DateOnly ClaimDate { get; set; }
        public string ClaimNumber { get; set; }
        public int ClaimUp { get; set; }
        public decimal ClaimAmount { get; set; }
        public DateOnly ResponseDate { get; set; }
        public string ResponseNumber { get; set; }
        public int PassA { get; set; }
        public int CancelC { get; set; }
        public int UserId { get; set; }
        public string Remark { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}