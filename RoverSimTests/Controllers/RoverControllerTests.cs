using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoverSim.Controllers;
using RoverSim.Models;
using RoverSim.Exceptions;

namespace RoverSimTests.Controllers
{
    [TestClass]
    public class RoverControllerTests
    {

        [TestMethod]
        public void RotateTest_WhenDirectionValid_ShouldRotate()
        {
            var rotateDirection = 'L';
            var orientation = Orientation.North;
            var expected = Orientation.West;
            var actual = RoverController.Rotate(rotateDirection, orientation);
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RoverConstructorTest_WhenRoverCoordinateIsOutOfRange_ShouldThrow()
        {
            var roverPosition = "6 6 E";
            var plateau = new Plateau(new Coordinate(5,5));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Rover(roverPosition, plateau));
        }

        [TestMethod]
        public void RotateTest_WhenDirectionInvalid_ShouldThrow()
        {
            var rotateDirection = 'N';
            var orientation = Orientation.West;
            
            Assert.ThrowsException<InvalidRotationArgumentException>(() => RoverController.Rotate(rotateDirection, orientation));
        }

        [TestMethod]
        public void ExecuteMovementPlan_WhenArgumentsValid_ShouldExecute()
        {
            var plateau = new Plateau(new Coordinate(5, 5));
            var rover = new Rover("1 2 N", plateau);
            var movementPlan = "LMLMLMLMM";
            var expected = new Rover("1 3 N", plateau);
            var actual = RoverController.ExecuteMovementPlan(movementPlan, rover, plateau);
            Assert.AreEqual(expected,actual,message:"");
        }

        [TestMethod]
        public void ExecuteMovementPlan_WhenPassedNullValues_ShouldThrow()
        {
            Assert.ThrowsException<ArgumentNullException>(() => RoverController.ExecuteMovementPlan(null,null,null));
        }

        [TestMethod]
        public void ExecuteMovementPlan_WhenMovementIsOutOfBounds_ShouldThrow()
        {
            var plateau = new Plateau(new Coordinate(5, 5));
            var rover = new Rover("1 2 E", plateau);
            var movementPlan = "MMMMMM";
            Assert.ThrowsException<InvalidMovementException>(() => RoverController.ExecuteMovementPlan(movementPlan,rover,plateau));
        }

        [TestMethod]
        public void ParseOrientation_WhenPassedEmptyString_ShouldThrow()
        {
            Assert.ThrowsException<ArgumentException>(() => RoverController.ParseOrientation(""));
        }

        [TestMethod]
        public void ParseOrientation_WhenPassedInvalidString_ShouldThrow()
        {
            var coord = "1 2 3 4";
            Assert.ThrowsException<ArgumentException>(() => RoverController.ParseOrientation(coord));
        }

        [TestMethod]
        public void ParseOrientation_WhenPassedInvalidDirection_ShouldThrow()
        {
            var coord = "1 2 B";
            Assert.ThrowsException<ArgumentException>(() => RoverController.ParseOrientation(coord));
        }

        [TestMethod]
        public void ParseOrientation_WhenPassedValidDirection_ShouldParse()
        {
            var coord = "1 4 N";
            var expected = Orientation.North;
            var actual = RoverController.ParseOrientation(coord);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ParseCoordinate_WhenPassedEmptyString_ShouldThrow()
        {
            Assert.ThrowsException<ArgumentException>(() => RoverController.ParseCoordinate(""));
        }

        [TestMethod]
        public void ParseCoordinate_WhenPassedInvalidString_ShouldThrow()
        {
            var coord = "1 2 3 4";
            Assert.ThrowsException<ArgumentException>(() => RoverController.ParseCoordinate(coord));
        }
        [TestMethod]
        public void ParseCoordinate_WhenPassedValidString_ShouldParse()
        {
            var coord = "1 4 N";
            var expected = new Coordinate(1,4);
            var actual = RoverController.ParseCoordinate(coord);
            Assert.AreEqual(expected, actual);        }
    }


}