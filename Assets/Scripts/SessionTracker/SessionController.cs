using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SessionTracker
{
    public class SessionController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _sessionText;

        private DateTime _sessionStartTime;
        private readonly List<string> _sessionHistory = new();

        private void Start()
        {
            _sessionStartTime = DateTime.Now;
            LoadPreviousSessions();
        }

        private void OnApplicationQuit()
        {
            var sessionEndTime = DateTime.Now;
            var sessionDuration = sessionEndTime - _sessionStartTime;
            var sessionRecord =
                $@"Start: {_sessionStartTime:hh\:mm\:ss}, End: {sessionEndTime:hh\:mm\:ss}, Duration: {sessionDuration:hh\:mm\:ss}";
            _sessionHistory.Add(sessionRecord);
            SaveSessionHistory();
        }

        private void SaveSessionHistory()
        {
            PlayerPrefs.SetInt("SessionCount", _sessionHistory.Count);
            for (var i = 0; i < _sessionHistory.Count; i++)
            {
                PlayerPrefs.SetString($"Session_{i}", _sessionHistory[i]);
                // PlayerPrefs.DeleteKey($"Session_{i}");
            }

            // PlayerPrefs.DeleteKey("SessionCount");
            PlayerPrefs.Save();
        }

        private void LoadPreviousSessions()
        {
            _sessionHistory.Clear();

            var sessionSummary = string.Empty;

            var sessionCount = PlayerPrefs.GetInt("SessionCount", 0);
            for (var i = 0; i < sessionCount; i++)
            {
                var sessionData = PlayerPrefs.GetString($"Session_{i}", "");
                if (string.IsNullOrEmpty(sessionData)) continue;

                _sessionHistory.Add(sessionData);
                sessionSummary += $"Previous session: {sessionData}\r\n";
            }

            _sessionText.text = sessionSummary;
        }
    }
}