﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class UITest
    {
        private bool sceneLoaded;

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {

            sceneLoaded = true;
        }

        [OneTimeSetUp]
        public void loadedLevel()
        {

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
        }


        [UnityTest]
        public IEnumerator PauseFunctionTest()
        {
            yield return new WaitWhile(() => sceneLoaded == false);

            var gobject = GameObject.Find("UI").GetComponent<PauseMenu>();
            var gameSpeed = Time.timeScale;

            Assert.AreEqual(gameSpeed, 1.0f);


            gobject.pauseControl();

            gameSpeed = Time.timeScale;
            Assert.AreEqual(gameSpeed, 0f);

            gobject.pauseControl();

            gameSpeed = Time.timeScale;
            Assert.AreEqual(gameSpeed, 1f);

            yield return null;

        }

        [UnityTest]
        public IEnumerator ScoreTest()
        {
            yield return new WaitWhile(() => sceneLoaded == false);

            var gobject = GameObject.Find("Score").GetComponent<UIManager>();
            var score = gobject.getScore();
            Assert.AreEqual(score, 0);

            gobject.addScore(15);

            score = gobject.getScore();
            Assert.AreEqual(score, 15);


            gobject.resetScore();
            score = gobject.getScore();
            Assert.AreEqual(score, 0);

            yield return null;

        }
    }
}
