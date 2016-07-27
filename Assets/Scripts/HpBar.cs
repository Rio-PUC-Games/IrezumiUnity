﻿using UnityEngine;

namespace Assets.Scripts
{
	public class HpBar : MonoBehaviour
    {
        #region Variables
		private float _value;           /*current value*/
		private float _fullBarValue;    /*value when bar is full*/
		public Vector2 Size;          /*size of bar*/
		public Vector2 ImageSize;
		public Vector2 Position;        /*position to create the bar*/
		public Texture2D EmptyBarImg;   /*image with empty bar*/
		public Texture2D FullBarImg;    /*image with full bar*/
		public Texture2D CriticalBarImg;
		public Texture2D ForegroundBarImg;
        public Player Player;
        #endregion

		public void Awake()
		{
			//Size = new Vector2(60, 10);
			//ImageSize = new Vector2(483, 79);
			Size = new Vector2(200, 50);
			Position = new Vector2(20, 40); 
		}

        public new void Start () {
            _fullBarValue = Player.Hp;
        }

        public void Update () {
            _value = Player.Hp;
        }

		/// <summary>
		/// Unity calls this function for rendering and handling GUI events.
		/// </summary>
		public void OnGUI () {
			/*background*/
			//GUI.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			GUI.BeginGroup (new Rect (Position, Size));
			GUI.Box (new Rect (0, 0, Size.x, Size.y), EmptyBarImg, new GUIStyle());


			/*full part*/
			GUI.BeginGroup (new Rect (0,0 , Size.x * (_value/_fullBarValue), Size.y));
			GUI.Box (new Rect (0, 0, Size.x, Size.y), _value > 20 ? FullBarImg : CriticalBarImg, new GUIStyle());


			GUI.EndGroup();
			/*foreground*/
			GUI.BeginGroup (new Rect (0, 0, Size.x, Size.y), ForegroundBarImg, new GUIStyle ());
			GUI.EndGroup();
			GUI.EndGroup();
		}
    }
}
