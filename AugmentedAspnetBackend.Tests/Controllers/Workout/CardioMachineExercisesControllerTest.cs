using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
                new CardioMachineExercise { },
                new CardioMachineExercise { } };
            String expected = "";
            String actual = controller.FullCardioMachineExerciseListCsv(list);
            Assert.AreEqual(expected, actual);
        }
    }
}
