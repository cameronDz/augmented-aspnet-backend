using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AugmentedAspnetBackend.Controllers.Workout;
using AugmentedAspnetBackend.Models.Workout;

namespace AugmentedAspnetBackend.Tests.Controllers.Workout
{
    [TestClass]
    public class CardioMachineExercisesControllerTest
    {
        [TestMethod]
        public void FullCardioMachineExerciseListCsv_Test()
        {
            CardioMachineExercisesController controller = new CardioMachineExercisesController();
            IEnumerable<CardioMachineExercise> list = new CardioMachineExercise[] {
                new CardioMachineExercise { Comment = "This, has a comma." },
                new CardioMachineExercise { Comment = "This field has a \"double quote\"." }
            };
            string expected = 
                "Id,Machine Type,Start Time,Duration Seconds,Distance Miles,User Name,Comment,\r\n" +
                "\"0\",,\"1/1/0001 12:00:00 AM\",\"0\",\"0\",,\"This, has a comma.\",\r\n" +
                "\"0\",,\"1/1/0001 12:00:00 AM\",\"0\",\"0\",,\"This field has a \"double quote\".\",\r\n";
            string actual = controller.FullCardioMachineExerciseListCsv(list);
            Assert.AreEqual(expected, actual);
        }
    }
}
