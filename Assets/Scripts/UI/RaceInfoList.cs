using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Plarium.Gamejam2019
{
    public class RaceInfoList : MonoBehaviour
    {
        [SerializeField] private VerticalLayoutGroup _layout;

        [SerializeField] private List<RaceInfo> _raceInfos;

        private void Awake()
        {
            //_raceInfos = new List<RaceInfo>();
        }

        public void StepUpdate()
        {
            foreach (var raceInfo in _raceInfos)
            {
                raceInfo.StepUpdate();
            }
        }

        public void AddRaceInfo(RaceInfo raceInfo)
        {
            _raceInfos.Add(raceInfo);
            raceInfo.transform.SetParent(transform);
        }
    }
}