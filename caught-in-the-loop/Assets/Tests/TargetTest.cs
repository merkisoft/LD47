using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using UnityEngine;

namespace Tests {
    public class TargetTest : ScriptableObject {
        [Test]
        public void Empty() {
            Assert.AreEqual("0 0", CreateInstance<Level>().compare(CreateInstance<Level>()));
        }

        [Test]
        public void MoreTarget1() {
            var targetLevel = CreateInstance<Level>();
            targetLevel.loopElements.Add(new LoopElement(null, 0.5f));
            Assert.AreEqual("1 0", CreateInstance<Level>().compare(targetLevel));
        }
        
        [Test]
        public void MoreUser1() {
            var targetLevel = CreateInstance<Level>();
            targetLevel.loopElements.Add(new LoopElement(null, 0.5f));
            Assert.AreEqual("0 1", targetLevel.compare(CreateInstance<Level>()));
        }
        
        [Test]
        public void Equal1() {
            var target = CreateInstance<Level>();
            target.loopTime = 1;
            var user = CreateInstance<Level>();
            target.loopElements.Add(new LoopElement(null, 0.45f));
            user.loopElements.Add(new LoopElement(null, 0.5f));
            target.loopElements.Add(new LoopElement(null, 0f));
            user.loopElements.Add(new LoopElement(null, 0.99f));
            Assert.AreEqual("0 0", user.compare(target));
        }
        
        [Test]
        public void Equal2() {
            var target = CreateInstance<Level>();
            target.loopTime = 1;
            var user = CreateInstance<Level>();
            target.loopElements.Add(new LoopElement(null, 0.45f));
            user.loopElements.Add(new LoopElement(null, 0.5f));
            target.loopElements.Add(new LoopElement(null, 0.99f));
            user.loopElements.Add(new LoopElement(null, 0f));
            Assert.AreEqual("0 0", user.compare(target));
        }
    }
}