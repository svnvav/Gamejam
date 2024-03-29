﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Plarium.Gamejam2019
{
    public class RaceInfoList : MonoBehaviour
    {
        [SerializeField] private VerticalLayoutGroup _layout;

        [SerializeField] private List<RaceInfo> _raceInfos;

        [SerializeField] private RaceInfo _raceInfoPrefab;

        public void StepUpdate()
        {
            foreach (var raceInfo in _raceInfos)
            {
                raceInfo.StepUpdate();
            }
        }

        public void AddRace(RaceOnPlanet raceOnPlanet)
        {
            var raceInfo = Instantiate(_raceInfoPrefab, transform);
            raceInfo.SetSprite(raceOnPlanet.Race.InfoSprite);
            raceInfo.RaceOnPlanet = raceOnPlanet;
            _raceInfos.Add(raceInfo);
        }

        public void RemoveRace(RaceOnPlanet raceOnPlanet)
        {
            var raceInfo = _raceInfos.Find(e => e.RaceOnPlanet == raceOnPlanet);
            if(raceInfo == null) return;
            _raceInfos.Remove(raceInfo);
            
            Destroy(raceInfo.gameObject);
        }
    }
}