using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using cpioli.Events;

namespace cpioli.Events
{
    public class CommonGameEventListenerList : MonoBehaviour
    {
        public GameEventListenerObj Pause;
        public GameEventListenerObj Resume;
        public GameEventListenerObj GameOver;
        public GameEventListenerObj LevelBegin;

        private UnityAction pauseActions;
        private UnityAction resumeActions;
        private UnityAction gameOverActions;
        private UnityAction levelBeginActions;

        private void OnEnable()
        {
            Pause.Event.RegisterListener(Pause);
            Resume.Event.RegisterListener(Resume);
            GameOver.Event.RegisterListener(GameOver);
            LevelBegin.Event.RegisterListener(LevelBegin);

            ICommonGameEvents[] commonGameEvents =
                transform.parent.gameObject.GetComponentsInChildren<ICommonGameEvents>();

            for (int i = commonGameEvents.Length - 1; i >= 0; i--)
            {
                //for every listener
                pauseActions += commonGameEvents[i].GamePaused;
                resumeActions += commonGameEvents[i].GameResumed;
                gameOverActions += commonGameEvents[i].GameOver;
                levelBeginActions += commonGameEvents[i].LevelStarted;
                //Pause.Response.AddListener(commonGameEvents[i].GamePaused);
            }
            Pause.Response.AddListener(pauseActions);
            Resume.Response.AddListener(resumeActions);
            GameOver.Response.AddListener(gameOverActions);
            LevelBegin.Response.AddListener(levelBeginActions);
        }

        private void OnDisable()
        {
            Pause.Response.RemoveListener(pauseActions);
            Resume.Response.RemoveListener(resumeActions);
            GameOver.Response.RemoveListener(gameOverActions);
            LevelBegin.Response.RemoveListener(levelBeginActions);
        }
    }
}

