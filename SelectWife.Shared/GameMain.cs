using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SelectWife
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameMain : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //֮ǰ��������״̬
        MouseState prevMouseState;

        //��������
        Song music;
        //��ť��Ч
        SoundEffect click;
        //�Ƿ���
        bool isMuted;

        //�Զ����������
        Texture2D mouseCursorTexture2D;
        //�Զ������λ�úʹ�С
        Rectangle mouseCursorRectangle;

        //��Ϸ�Ŀ�Ⱥ͸߶�
        int GameWidth = 480;
        int GameHeight = 800;

        //��ǰ����
        GameScence currentScence;

        //��Ϸ��������
        Texture2D gameBackgroundTexture2D;
        //��Ϸ����λ�úʹ�С
        Rectangle gameBackgroundRectangle;

        //��������
        Texture2D gameTitleTexture2D;
        //����λ�úʹ�С
        Rectangle gameTitleRectangle;

        //���忪ʼ��ť����
        Texture2D playNormalTexture2D;
        //���忪ʼ��ť��λ�úʹ�С
        Rectangle playNormalRectangle;

        //��Ч���ذ�ť�������λ��
        Texture2D soundOnTexture2D;
        //������ť������
        Texture2D soundOffTexture2D;
        Rectangle soundOnRectangle;
        //���ڰ�ť�������λ��
        Texture2D aboutTexture2D;
        Rectangle aboutRectangle;

        //���ڳ������õ���������Դ��λ���Լ���������
        SpriteFont aboutFont;
        Vector2 aboutFontPosition;
        string aboutText = "��Ϸ���ƣ�ѡ����\n\r���ߣ�chengcong\n\r��Ȩ���У�www.xnadevelop.com";

        //���巵�ذ�ť����ͼƬ��λ�ô�С
        Texture2D backTexture2D;
        Rectangle backRectangle;


        List<Texture2D> wifeTextureList;//����ͼƬ�б�
        Rectangle wifeListRectangle;//ͼƬλ�úʹ�С
        int currentFrame = 0;// ��ǰͼƬ����������ʾ��ǰ��ʾ�����ţ�Ĭ���ǵ�һ��

        int timeLastFrame = 0;//ÿ���л�ͼƬ�󾭹���ʱ��
        int timePerFame = 100;//ÿ100�����л�һ��,Ĭ����16.666�����л�һ�Σ�������ˢ����ÿ��60֡

        //ѡ���Ű�ť�������λ�ô�С
        Texture2D marryHerTexture2D;
        Rectangle marryHerRectangle;

        //�л����ſ���
        bool starting = false;

        //˫ϲͼƬ�����λ�ô�С
        Texture2D doubleHappinessTexture2D;
        Rectangle doubleHappinessRectangle;
        //��鰴ťͼƬ�����λ�ô�С
        Texture2D divorceTexture2D;
        Rectangle divorceRectangle;
        //���˵���ťͼƬ�����λ�ô�С
        Texture2D mainMenuTexture2D;
        Rectangle mainMenuRectangle;

        //�����������
        Song weddingMarch;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = GameWidth;
            graphics.PreferredBackBufferHeight = GameHeight;
            //��Ϸ���ڱ���
            this.Window.Title = "ѡ����";
            IsMouseVisible = false;
            Microsoft.Xna.Framework.Input.Touch.TouchPanel.EnableMouseTouchPoint = true;

            graphics.IsFullScreen = true;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //ScalingClever.ResolutionScaling.Initialize(this, new Point(480, 800));
            base.Initialize();
        }
        protected override bool BeginDraw()
        {
            ScalingClever.ResolutionScaling.Initialize(this,new Point(480, 800));
            ScalingClever.ResolutionScaling.BeginDraw(this);
            return base.BeginDraw();
        }
        protected override void EndDraw()
        {
            ScalingClever.ResolutionScaling.EndDraw(this, spriteBatch);
            base.EndDraw();
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //���ر������ֺͰ�ť��Ч
            music = Content.Load<Song>("Music");
            click = Content.Load<SoundEffect>("Click");
            //Ĭ������Ϊ������
            isMuted = false;
            //���ű�������
            MediaPlayer.Play(music);
            //���ñ������ֵĴ�С
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;//�ظ����ű�������


            //����������������С
            mouseCursorTexture2D = Content.Load<Texture2D>("MouseCursor");
            mouseCursorRectangle = new Rectangle(0, 0, 45, 45);

            currentScence = GameScence.MainMenu;
            // TODO: use this.Content to load your game content here

            //������Ϸ����ͼƬ����
            gameBackgroundTexture2D = Content.Load<Texture2D>("GameBackground");
            //��Ϸ������480x800��ȫ������
            gameBackgroundRectangle = new Rectangle(0, 0, 480, 800);

            //���ر���ͼƬ����
            gameTitleTexture2D = Content.Load<Texture2D>("GameTitle");
            //��ʼ������λ�úʹ�С
            gameTitleRectangle = new Rectangle((GameWidth - gameTitleTexture2D.Bounds.Width) / 2, 100, gameTitleTexture2D.Bounds.Width, gameTitleTexture2D.Bounds.Height);

            //���ؿ�ʼ�������
            playNormalTexture2D = Content.Load<Texture2D>("PlayNormal");
            //��ʼ����ʼ��ťλ�úʹ�С
            playNormalRectangle = new Rectangle((GameWidth - playNormalTexture2D.Bounds.Width) / 2, 280, playNormalTexture2D.Bounds.Width, playNormalTexture2D.Bounds.Height);

            //������Ч���غ͹��ڰ�ť�����λ�ô�С
            soundOnTexture2D = Content.Load<Texture2D>("SoundOn");
            soundOnRectangle = new Rectangle(100, 580, 75, 75);
            aboutTexture2D = Content.Load<Texture2D>("About");
            aboutRectangle = new Rectangle(320, 580, 75, 75);
            //���ؾ�������
            soundOffTexture2D = Content.Load<Texture2D>("SoundOff");


            //���ع�������λ�úʹ�С�Լ�����
            aboutFont = Content.Load<SpriteFont>("AboutFont");
            Vector2 aboutTextVector2 = aboutFont.MeasureString(aboutText);
            aboutFontPosition = new Vector2((GameWidth - (int)aboutTextVector2.X) / 2, 300);

            //���ط��ذ�ť��ͼƬ�����λ�ô�С
            backTexture2D = Content.Load<Texture2D>("Back");
            backRectangle = new Rectangle(20, 20, 50, 50);

            //����ͼƬ�б�
            wifeTextureList = new List<Texture2D>();
            for (int i = 1; i < 12; i++)
            {
                Texture2D wifeTexture = Content.Load<Texture2D>("Wife" + i);

                wifeTextureList.Add(wifeTexture);
            }
            //����ͼƬ�б�λ��
            wifeListRectangle = new Rectangle((GameWidth - 295) / 2, 100, 295, 221);

            //����ѡ���Ű�ť�ͳ�ʼ��λ�ô�С
            marryHerTexture2D = Content.Load<Texture2D>("MarryHer");
            marryHerRectangle = new Rectangle((GameWidth - marryHerTexture2D.Bounds.Width) / 2, 300, marryHerTexture2D.Bounds.Width, marryHerTexture2D.Bounds.Height);

            //����˫ϲͼƬ����ͳ�ʼ��λ�ô�С
            doubleHappinessTexture2D = Content.Load<Texture2D>("DoubleHappiness");
            doubleHappinessRectangle = new Rectangle((GameWidth - doubleHappinessTexture2D.Bounds.Width) / 2, 330, doubleHappinessTexture2D.Bounds.Width, doubleHappinessTexture2D.Bounds.Height);

            //�������ͼƬ����ͳ�ʼ��λ�ô�С
            divorceTexture2D = Content.Load<Texture2D>("Divorce");
            divorceRectangle = new Rectangle(50, 640, divorceTexture2D.Bounds.Width, divorceTexture2D.Bounds.Height);
            //�������˵�ͼƬ����ͳ�ʼ��λ�ô�С
            mainMenuTexture2D = Content.Load<Texture2D>("MainMenu");
            mainMenuRectangle = new Rectangle(280, 640, mainMenuTexture2D.Bounds.Width, mainMenuTexture2D.Bounds.Height);

            //���ؽ�������
            weddingMarch = Content.Load<Song>("WeddingMarch");



            ScalingClever.ResolutionScaling.LoadContent(this, new Point(480, 800));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here



            var touches = Microsoft.Xna.Framework.Input.Touch.TouchPanel.GetState();
           
            foreach (var touch in touches)
            {
              
                if (touch.State != Microsoft.Xna.Framework.Input.Touch.TouchLocationState.Released)
                {

                    System.Diagnostics.Debug.WriteLine(touch.State.ToString());


                    var postion = ScalingClever.ResolutionScaling.Position(touch.Position);

                    var X = ScalingClever.ResolutionScaling.X(touch.Position.X);

                    var Y = ScalingClever.ResolutionScaling.Y(touch.Position.Y);

                    //�޸��Զ��崥��λ�õ�X��Y������Ϊ��굱ǰX,Yλ��
                    mouseCursorRectangle.X = (int)X;
                    mouseCursorRectangle.Y = (int)Y;

                    //�жϵ�ǰ�����Ƿ������˵�
                    if (currentScence == GameScence.MainMenu)
                    {
                        //�ж��Ƿ����˹��ڰ�ť
                        if ((aboutRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y)))
                        {
                            //����ǰ�����л����ڳ���
                            currentScence = GameScence.About;
                            //��ť�����Ч
                            if (!isMuted)
                            {
                                click.Play();
                            }
                        }
                        //�ж��Ƿ����ر���Ч���ְ�ť
                        if ( soundOnRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y))
                        {
                            isMuted = !isMuted;
                            MediaPlayer.IsMuted = isMuted;
                            //��ť�����Ч
                            if (!isMuted)
                            {
                                click.Play();
                            }
                        }
                        //�ж��Ƿ����˿�ʼ��Ϸ��ť
                        if ( playNormalRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y))
                        {
                            //��ת����Ϸ����
                            currentScence = GameScence.GamePlaying;

                            starting = true;
                            //��ť�����Ч
                            if (!isMuted)
                            {
             
                                click.Play();
                            }


                        }

                    }

                    //�жϵ�ǰ�����Ƿ�����Ϸ����
                    else if (currentScence == GameScence.GamePlaying)
                    {
                        //�ж��Ƿ����˹��ڰ�ť
                        if (backRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y))
                        {
                            //����ǰ�����л������˵�
                            currentScence = GameScence.MainMenu;
                            //��ť�����Ч
                            if (!isMuted)
                            {
                                click.Play();
                            }

                        }

                        //�ж��Ƿ�����ѡ���Ű�ť
                        if (marryHerRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y))
                        {
                            //ֹͣ�л�ͼƬ
                            starting = false;
                            //�л�����Ϸ����ҳ��
                            currentScence = GameScence.GameOver;
                            if (!isMuted)
                            {
                                //ֹ֮ͣǰ����Ϸ����
                                MediaPlayer.Stop();
                                //���Ž�������
                                MediaPlayer.Play(weddingMarch);
                            }
                            //��ť�����Ч
                            if (!isMuted)
                            {
                                click.Play();
                            }

                        }

                       

                    }

                    //�жϵ�ǰ�����Ƿ��ǹ���
                    else if (currentScence == GameScence.About)
                    {
                        //�ж��Ƿ����˹��ڰ�ť
                        if ( backRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y))
                        {
                            //����ǰ�����л������˵�
                            currentScence = GameScence.MainMenu;
                            //��ť�����Ч
                            if (!isMuted)
                            {
                                click.Play();
                            }
                        }

                    }
                    else if (currentScence == GameScence.GameOver)
                    {

                        //�ж��Ƿ��������
                        if ( divorceRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y))
                        {
                            //ֹͣ�л�ͼƬ
                            starting = true;
                            //�л�����Ϸҳ��
                            currentScence = GameScence.GamePlaying;
                            if (!isMuted)
                            {
                                //ֹ֮ͣǰ�Ľ�������
                                MediaPlayer.Stop();
                                //���Ž�������
                                MediaPlayer.Play(music);
                            }
                            //��ť�����Ч
                            if (!isMuted)
                            {
                                click.Play();
                            }
                        }
                        //�ж��Ƿ��������˵�
                        if ( mainMenuRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y))
                        {
                            //�л�����Ϸҳ��
                            currentScence = GameScence.MainMenu;

                            if (!isMuted)
                            {
                                //ֹ֮ͣǰ�Ľ�������
                                MediaPlayer.Stop();
                                //���Ž�������
                                MediaPlayer.Play(music);
                            }
                            //��ť�����Ч
                            if (!isMuted)
                            {
                                click.Play();
                            }
                        }

                    }
                    //���״̬��ֵ��ǰ���״̬
                    //prevMouseState = mouseState;
                    mouseCursorRectangle = new Rectangle();



                }

            }

            if (starting)
            {
                timeLastFrame = timeLastFrame + gameTime.ElapsedGameTime.Milliseconds;//ͼƬ�л��󾭹���ʱ�䣨���룩
                if (timeLastFrame > timePerFame)
                {
                    timeLastFrame = timeLastFrame - timePerFame;//��ͼƬ�л��󾭹���ʱ��ָ���С��ÿ���л�ʱ�䣬��֤�������ִ��һ��
                    if (currentFrame >= wifeTextureList.Count - 1)
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        currentFrame += 1;
                    }
                }
            }

            ////��ȡ��ǰ���״̬
            //MouseState mouseState = Mouse.GetState();
            ////�޸��Զ������λ�õ�X��Y������Ϊ��굱ǰX,Yλ��
            //mouseCursorRectangle.X = mouseState.X;
            //mouseCursorRectangle.Y = mouseState.Y;

            ////�жϵ�ǰ�����Ƿ������˵�
            //if (currentScence == GameScence.MainMenu)
            //{
            //    //�ж��Ƿ����˹��ڰ�ť
            //    if ((mouseState != prevMouseState && mouseState.LeftButton != ButtonState.Released && aboutRectangle.Contains(mouseState.X, mouseState.Y))||)
            //    {
            //        //����ǰ�����л����ڳ���
            //        currentScence = GameScence.About;
            //        //��ť�����Ч
            //        if(!isMuted)
            //        {
            //            click.Play();
            //        }
            //    }
            //    //�ж��Ƿ����ر���Ч���ְ�ť
            //    if (mouseState != prevMouseState && mouseState.LeftButton != ButtonState.Released && soundOnRectangle.Contains(mouseState.X, mouseState.Y))
            //    {
            //        isMuted =! isMuted;
            //        MediaPlayer.IsMuted = isMuted;
            //        //��ť�����Ч
            //        if (!isMuted)
            //        {
            //            click.Play();
            //        }
            //    }
            //    //�ж��Ƿ����˿�ʼ��Ϸ��ť
            //    if (mouseState != prevMouseState && mouseState.LeftButton != ButtonState.Released && playNormalRectangle.Contains(mouseState.X, mouseState.Y))
            //    {
            //        //��ת����Ϸ����
            //        currentScence = GameScence.GamePlaying;

            //        starting = true;
            //        //��ť�����Ч
            //        if (!isMuted)
            //        {
            //            click.Play();
            //        }


            //    }

            //}

            ////�жϵ�ǰ�����Ƿ�����Ϸ����
            //else if (currentScence == GameScence.GamePlaying)
            //{
            //    //�ж��Ƿ����˹��ڰ�ť
            //    if (mouseState != prevMouseState && mouseState.LeftButton != ButtonState.Released && backRectangle.Contains(mouseState.X, mouseState.Y))
            //    {
            //        //����ǰ�����л������˵�
            //        currentScence = GameScence.MainMenu;
            //        //��ť�����Ч
            //        if (!isMuted)
            //        {
            //            click.Play();
            //        }

            //    }

            //    //�ж��Ƿ�����ѡ���Ű�ť
            //    if (mouseState!=prevMouseState&& mouseState.LeftButton != ButtonState.Released && marryHerRectangle.Contains(mouseState.X, mouseState.Y))
            //    {
            //        //ֹͣ�л�ͼƬ
            //        starting = false;
            //        //�л�����Ϸ����ҳ��
            //        currentScence = GameScence.GameOver;
            //        if (!isMuted)
            //        {
            //            //ֹ֮ͣǰ����Ϸ����
            //            MediaPlayer.Stop();
            //            //���Ž�������
            //            MediaPlayer.Play(weddingMarch);
            //        }
            //        //��ť�����Ч
            //        if (!isMuted)
            //        {
            //            click.Play();
            //        }

            //    }

            //    if (starting)
            //    {
            //        timeLastFrame = timeLastFrame + gameTime.ElapsedGameTime.Milliseconds;//ͼƬ�л��󾭹���ʱ�䣨���룩
            //        if (timeLastFrame > timePerFame)
            //        {
            //            timeLastFrame = timeLastFrame - timePerFame;//��ͼƬ�л��󾭹���ʱ��ָ���С��ÿ���л�ʱ�䣬��֤�������ִ��һ��
            //            if (currentFrame >= wifeTextureList.Count - 1)
            //            {
            //                currentFrame = 0;
            //            }
            //            else
            //            {
            //                currentFrame += 1;
            //            }
            //        }
            //    }

            //    }

            ////�жϵ�ǰ�����Ƿ��ǹ���
            //else if(currentScence == GameScence.About)
            //{
            //    //�ж��Ƿ����˹��ڰ�ť
            //    if (mouseState != prevMouseState && mouseState.LeftButton != ButtonState.Released && backRectangle.Contains(mouseState.X, mouseState.Y))
            //    {
            //        //����ǰ�����л������˵�
            //        currentScence = GameScence.MainMenu;
            //        //��ť�����Ч
            //        if (!isMuted)
            //        {
            //            click.Play();
            //        }
            //    }

            //}
            //else if (currentScence == GameScence.GameOver)
            //{

            //    //�ж��Ƿ��������
            //    if (mouseState != prevMouseState && mouseState.LeftButton != ButtonState.Released && divorceRectangle.Contains(mouseState.X, mouseState.Y))
            //    {
            //        //ֹͣ�л�ͼƬ
            //        starting = true;
            //        //�л�����Ϸҳ��
            //        currentScence = GameScence.GamePlaying;
            //        if (!isMuted)
            //        {
            //            //ֹ֮ͣǰ�Ľ�������
            //            MediaPlayer.Stop();
            //            //���Ž�������
            //            MediaPlayer.Play(music);
            //        }
            //        //��ť�����Ч
            //        if (!isMuted)
            //        {
            //            click.Play();
            //        }
            //    }
            //    //�ж��Ƿ��������˵�
            //    if (mouseState != prevMouseState && mouseState.LeftButton != ButtonState.Released && mainMenuRectangle.Contains(mouseState.X, mouseState.Y))
            //    {
            //        //�л�����Ϸҳ��
            //        currentScence = GameScence.MainMenu;

            //        if (!isMuted)
            //        {
            //            //ֹ֮ͣǰ�Ľ�������
            //            MediaPlayer.Stop();
            //            //���Ž�������
            //            MediaPlayer.Play(music);
            //        }
            //        //��ť�����Ч
            //        if (!isMuted)
            //        {
            //            click.Play();
            //        }
            //    }

            //}
            ////���״̬��ֵ��ǰ���״̬
            //prevMouseState = mouseState;


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //������Ϸ����
            spriteBatch.Begin();
            spriteBatch.Draw(gameBackgroundTexture2D, gameBackgroundRectangle, Color.White);
            spriteBatch.End();

            if (currentScence == GameScence.MainMenu)
            {

                spriteBatch.Begin();
                //���˵��������ƽ���
                spriteBatch.Draw(gameTitleTexture2D, gameTitleRectangle, Color.White);
                //���ƿ�ʼ��ť
                spriteBatch.Draw(playNormalTexture2D, playNormalRectangle, Color.White);

                //������Ч���غ͹��ڰ�ť
                if (isMuted)
                {
                    //����
                    spriteBatch.Draw(soundOffTexture2D, soundOnRectangle, Color.White);
                }
                else
                {
                    spriteBatch.Draw(soundOnTexture2D, soundOnRectangle, Color.White);
                }
                spriteBatch.Draw(aboutTexture2D, aboutRectangle, Color.White);

                spriteBatch.End();
            }
            else if (currentScence == GameScence.GamePlaying)
            {

                spriteBatch.Begin();
                //��Ϸ�������ƽ���
                //���Ʒ��ذ�ť
                spriteBatch.Draw(backTexture2D, backRectangle, Color.White);

                //��������ͼ��
                spriteBatch.Draw(wifeTextureList[currentFrame], wifeListRectangle, Color.White);

                //����ѡ���Ű�ť
                spriteBatch.Draw(marryHerTexture2D, marryHerRectangle, Color.White);

                spriteBatch.End();
            }
            else if (currentScence == GameScence.GameOver)
            {

                spriteBatch.Begin();
                //��Ϸ�������ƽ���
                //��������ͼ��
                spriteBatch.Draw(wifeTextureList[currentFrame], wifeListRectangle, Color.White);
                //����˫ϲͼƬ
                spriteBatch.Draw(doubleHappinessTexture2D, doubleHappinessRectangle, Color.White);
                //������鰴ť
                spriteBatch.Draw(divorceTexture2D, divorceRectangle, Color.White);
                //�������˵���ť
                spriteBatch.Draw(mainMenuTexture2D, mainMenuRectangle, Color.White);
                spriteBatch.End();
            }
            else if (currentScence == GameScence.About)
            {

                spriteBatch.Begin();
                //���ڳ������ƽ���
                spriteBatch.DrawString(aboutFont, aboutText, aboutFontPosition, Color.Orange);
                //���Ʒ��ذ�ť
                spriteBatch.Draw(backTexture2D, backRectangle, Color.White);
                spriteBatch.End();
            }

            spriteBatch.Begin();
            //�����Զ������
            spriteBatch.Draw(mouseCursorTexture2D, mouseCursorRectangle, Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

    }
}
