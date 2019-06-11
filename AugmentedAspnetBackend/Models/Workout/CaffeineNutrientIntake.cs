using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace AugmentedAspnetBackend.Models.Workout
{
    public class CaffeineNutrientIntake
    {
        [Key]
        [Display(Name = "Cardio Machine Exercise Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CaffeineNutrientIntakeId { get; set; }
        [Display(Name = "Amount Type")]
        public String AmountType { get; set; }
        [Display(Name = "Amount")]
        public int Amount { get; set; }
        [Display(Name = "Intake Time")]
        public DateTime IntakeTime { get; set; }
        [Display(Name = "User Name")]
        public String UserName { get; set; }
        [Display(Name = "Comment")]
        public String Comment { get; set; }
    }
}
