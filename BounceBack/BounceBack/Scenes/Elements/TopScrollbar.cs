using Annex;
using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Scenes.Components;
using BounceBack.Models;
using System;
using System.Collections.Generic;

namespace BounceBack.Scenes.Elements
{
    public class TopScrollbar : UIElement
    {
        private int numOfWantedPeople = 5;
        private int numOfDescription = 4;

        private TextureContext _leftArrow;
        private TextureContext _rightArrow;

        private List<Person> _bannedPeople;
        private List<TextureContext> _bannedPeoplePicture;
        private List<List<TextContext>> _bannedPeopleDescription;


        public TopScrollbar() : base("")
        {
            this._leftArrow = new TextureContext("icon-1.png")
            {
                RenderPosition = Vector.Create(0, 0),
                RenderSize = Vector.Create(100, 100),
                UseUIView = true
            };

            this._rightArrow = new TextureContext("icon-1.png")
            {
                RenderPosition = Vector.Create(ServiceProvider.Canvas.GetResolution().X - 100, 0),
                RenderSize = Vector.Create(100, 100),
                UseUIView = true
            };

            this._bannedPeople = new List<Person>();
            this._bannedPeoplePicture = new List<TextureContext>();
            this._bannedPeopleDescription = new List<List<TextContext>>();

            
            /*
            for (int i = 0; i < numOfWantedPeople; i++)
            {
                this._bannedPeoplePicture.Add(new TextureContext("blank.png")
                {
                    RenderPosition = Vector.Create(100 + 150 * i, 0),
                    RenderSize = Vector.Create(50, 50),
                    UseUIView = true
                });

                //this._bannedPeopleDescription[i] = new List<TextContext>();
                this._bannedPeopleDescription.Add(new List<TextContext>());
                for (int j = 0; j < numOfDescription; j++)
                {
                    //this._bannedPeopleDescription[i, j]
                    this._bannedPeopleDescription[i].Add(new TextContext("Test: ", "default.ttf")
                    {
                        RenderPosition = Vector.Create(150 + 150 * i, 20 * j),
                        FontColor = RGBA.Black,
                        UseUIView = true,
                        FontSize = 14
                    });
                }
            }
            */
        }

        public override void Draw(ICanvas canvas)
        {
            for (int i = 0; i < this._bannedPeoplePicture.Count; i++)
            {
                canvas.Draw(this._bannedPeoplePicture[i]);

                for (int j = 0; j < numOfDescription; j++)
                {                    
                    canvas.Draw(this._bannedPeopleDescription[i][j]);
                }
            }
            canvas.Draw(this._leftArrow);
            canvas.Draw(this._rightArrow);
        }

        public void PressedOnButtons(float posX, float posY)
        {
            if (posX <= ServiceProvider.Canvas.GetResolution().X / 5 && posY <= 100)
            {
                HandleLeftButton();
            }
            else if (posX >= ServiceProvider.Canvas.GetResolution().X * 4 / 5 && posY <= 100)
            {
                HandleRightButton();
            }
        }

        public void HandleLeftButton()
        {
            for (int i = 0; i < this._bannedPeoplePicture.Count; i++)
            {
                this._bannedPeoplePicture[i].RenderPosition.Set(this._bannedPeoplePicture[i].RenderPosition.X - 100, this._bannedPeoplePicture[i].RenderPosition.Y);
            }
        }

        public void HandleRightButton()
        {
            for (int i = 0; i < this._bannedPeoplePicture.Count; i++)
            {
                this._bannedPeoplePicture[i].RenderPosition.Set(this._bannedPeoplePicture[i].RenderPosition.X + 100, this._bannedPeoplePicture[i].RenderPosition.Y);
            }
        }

        public void UpdateBar(List<Person> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                //Check if person is same
                if (_bannedPeople.Contains(list[i]))
                {
                    continue;
                }

                _bannedPeople.Add(list[i]);
            }

            this._bannedPeoplePicture = new List<TextureContext>();
            this._bannedPeopleDescription = new List<List<TextContext>>();


            for(int i = 0; i < _bannedPeople.Count; i++)
            {               
                this._bannedPeoplePicture.Add(new TextureContext("blank.png")
                {
                    RenderPosition = Vector.Create(50 + 150 * i, 0),
                    RenderSize = Vector.Create(50, 50),
                    UseUIView = true
                });

                this._bannedPeopleDescription.Add(new List<TextContext>());
                for (int j = 0; j < numOfDescription; j++)
                {
                    string description = "";

                    var arr = Enum.GetValues(typeof(VisibleFeatureType));
                    VisibleFeatureType feature = (VisibleFeatureType)arr.GetValue(RNG.Next(0, arr.Length - 1));

                    if (_bannedPeople[i].GetFeature(feature) != null)
                    {
                        description = _bannedPeople[i].GetFeature(feature).TextureContext.SourceTextureName;
                        description = description.Substring(description.IndexOf('/') + 1);
                        description = description.Substring(0, description.LastIndexOf('.'));
                    }

                    this._bannedPeopleDescription[i].Add(new TextContext(description, "default.ttf")
                    {
                        RenderPosition = Vector.Create(100 + 150 * i, 20 * j),
                        FontColor = RGBA.Black,
                        UseUIView = true,
                        FontSize = 14
                    });
                }
            }                                                                          
                        
        }
    }
}