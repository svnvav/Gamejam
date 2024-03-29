﻿using UnityEngine;

namespace Plarium.Gamejam2019
{
    [CreateAssetMenu]
    public class Race : ScriptableObject
    {
        [SerializeField] private string _name;
        public string Name => _name;

        [SerializeField] private string _description;
        public string Description => _description;

        [SerializeField] private Color _color;
        public Color Color => _color;
        
        [SerializeField] private Sprite _infoSprite;
        public Sprite InfoSprite => _infoSprite;

        [SerializeField] private Sprite _planetSprite;
        public Sprite PlanetSprite => _planetSprite;
        
        [SerializeField] private Sprite _helloSprite;
        public Sprite HelloSprite => _helloSprite;

        [SerializeField] private bool _isAngry;

        public bool IsAngry => _isAngry;
    }
}