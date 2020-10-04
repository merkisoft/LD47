using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using UnityEngine;

namespace Tests {
    public class TargetTest : ScriptableObject {
        [Test]
        public void Empty() {
            Assert.AreEqual("0 0", TargetSoundLoop.compare(new List<LoopElement>(), new List<LoopElement>(), 1));
        }

        [Test]
        public void MoreTarget1() {
            var target = new List<LoopElement>();
            var user = new List<LoopElement>();
            target.Add(new LoopElement(null, 0.5f));
            Assert.AreEqual("1 0", TargetSoundLoop.compare(user, target, 1));
        }

        [Test]
        public void MoreUser1() {
            var target = new List<LoopElement>();
            var user = new List<LoopElement>();
            user.Add(new LoopElement(null, 0.5f));
            Assert.AreEqual("0 1", TargetSoundLoop.compare(user, target, 1));
        }
        
        [Test]
        public void Equal1() {
            var target = new List<LoopElement>();
            var user = new List<LoopElement>();
            target.Add(new LoopElement(null, 0.45f));
            user.Add(new LoopElement(null, 0.5f));
            target.Add(new LoopElement(null, 0f));
            user.Add(new LoopElement(null, 0.99f));
            Assert.AreEqual("0 0", TargetSoundLoop.compare(user, target, 1));
        }
        
        [Test]
        public void Equal2() {
            var target = new List<LoopElement>();
            var user = new List<LoopElement>();
            target.Add(new LoopElement(null, 0.45f));
            user.Add(new LoopElement(null, 0.5f));
            target.Add(new LoopElement(null, 0.99f));
            user.Add(new LoopElement(null, 0f));
            Assert.AreEqual("0 0", TargetSoundLoop.compare(user, target, 1));
        }
    }
}