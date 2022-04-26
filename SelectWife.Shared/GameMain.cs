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

        //之前点击的鼠标状态
        MouseState prevMouseState;

        //背景音乐
        Song music;
        //按钮音效
        SoundEffect click;
        //是否静音
        bool isMuted;

        //自定义鼠标纹理
        Texture2D mouseCursorTexture2D;
        //自定义鼠标位置和大小
        Rectangle mouseCursorRectangle;

        //游戏的宽度和高度
        int GameWidth = 480;
        int GameHeight = 800;

        //当前场景
        GameScence currentScence;

        //游戏背景纹理
        Texture2D gameBackgroundTexture2D;
        //游戏背景位置和大小
        Rectangle gameBackgroundRectangle;

        //标题纹理
        Texture2D gameTitleTexture2D;
        //标题位置和大小
        Rectangle gameTitleRectangle;

        //定义开始按钮纹理
        Texture2D playNormalTexture2D;
        //定义开始按钮的位置和大小
        Rectangle playNormalRectangle;

        //声效开关按钮的纹理和位置
        Texture2D soundOnTexture2D;
        //静音按钮的纹理
        Texture2D soundOffTexture2D;
        Rectangle soundOnRectangle;
        //关于按钮的纹理和位置
        Texture2D aboutTexture2D;
        Rectangle aboutRectangle;

        //关于场景中用到的文字资源和位置以及文字内容
        SpriteFont aboutFont;
        Vector2 aboutFontPosition;
        string aboutText = "游戏名称：选老婆\n\r作者：chengcong\n\r版权所有：www.xnadevelop.com";

        //定义返回按钮纹理图片和位置大小
        Texture2D backTexture2D;
        Rectangle backRectangle;


        List<Texture2D> wifeTextureList;//定义图片列表
        Rectangle wifeListRectangle;//图片位置和大小
        int currentFrame = 0;// 当前图片索引，来显示当前显示的老婆，默认是第一张

        int timeLastFrame = 0;//每次切换图片后经过的时间
        int timePerFame = 100;//每100毫秒切换一次,默认是16.666毫秒切换一次，即画面刷新率每秒60帧

        //选老婆按钮的纹理和位置大小
        Texture2D marryHerTexture2D;
        Rectangle marryHerRectangle;

        //切换老婆开关
        bool starting = false;

        //双喜图片纹理和位置大小
        Texture2D doubleHappinessTexture2D;
        Rectangle doubleHappinessRectangle;
        //离婚按钮图片纹理和位置大小
        Texture2D divorceTexture2D;
        Rectangle divorceRectangle;
        //主菜单按钮图片纹理和位置大小
        Texture2D mainMenuTexture2D;
        Rectangle mainMenuRectangle;

        //定义结婚进行曲
        Song weddingMarch;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = GameWidth;
            graphics.PreferredBackBufferHeight = GameHeight;
            //游戏窗口标题
            this.Window.Title = "选老婆";
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

            //加载背景音乐和按钮音效
            music = Content.Load<Song>("Music");
            click = Content.Load<SoundEffect>("Click");
            //默认设置为不静音
            isMuted = false;
            //播放背景音乐
            MediaPlayer.Play(music);
            //设置背景音乐的大小
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.IsRepeating = true;//重复播放背景音乐


            //加载鼠标纹理及坐标大小
            mouseCursorTexture2D = Content.Load<Texture2D>("MouseCursor");
            mouseCursorRectangle = new Rectangle(0, 0, 45, 45);

            currentScence = GameScence.MainMenu;
            // TODO: use this.Content to load your game content here

            //加载游戏背景图片纹理
            gameBackgroundTexture2D = Content.Load<Texture2D>("GameBackground");
            //游戏界面是480x800，全屏覆盖
            gameBackgroundRectangle = new Rectangle(0, 0, 480, 800);

            //加载标题图片纹理
            gameTitleTexture2D = Content.Load<Texture2D>("GameTitle");
            //初始化标题位置和大小
            gameTitleRectangle = new Rectangle((GameWidth - gameTitleTexture2D.Bounds.Width) / 2, 100, gameTitleTexture2D.Bounds.Width, gameTitleTexture2D.Bounds.Height);

            //加载开始那妞纹理
            playNormalTexture2D = Content.Load<Texture2D>("PlayNormal");
            //初始化开始按钮位置和大小
            playNormalRectangle = new Rectangle((GameWidth - playNormalTexture2D.Bounds.Width) / 2, 280, playNormalTexture2D.Bounds.Width, playNormalTexture2D.Bounds.Height);

            //加载声效开关和关于按钮纹理和位置大小
            soundOnTexture2D = Content.Load<Texture2D>("SoundOn");
            soundOnRectangle = new Rectangle(100, 580, 75, 75);
            aboutTexture2D = Content.Load<Texture2D>("About");
            aboutRectangle = new Rectangle(320, 580, 75, 75);
            //加载静音纹理
            soundOffTexture2D = Content.Load<Texture2D>("SoundOff");


            //加载关于字体位置和大小以及内容
            aboutFont = Content.Load<SpriteFont>("AboutFont");
            Vector2 aboutTextVector2 = aboutFont.MeasureString(aboutText);
            aboutFontPosition = new Vector2((GameWidth - (int)aboutTextVector2.X) / 2, 300);

            //加载返回按钮的图片纹理和位置大小
            backTexture2D = Content.Load<Texture2D>("Back");
            backRectangle = new Rectangle(20, 20, 50, 50);

            //加载图片列表
            wifeTextureList = new List<Texture2D>();
            for (int i = 1; i < 12; i++)
            {
                Texture2D wifeTexture = Content.Load<Texture2D>("Wife" + i);

                wifeTextureList.Add(wifeTexture);
            }
            //定义图片列表位置
            wifeListRectangle = new Rectangle((GameWidth - 295) / 2, 100, 295, 221);

            //加载选老婆按钮和初始化位置大小
            marryHerTexture2D = Content.Load<Texture2D>("MarryHer");
            marryHerRectangle = new Rectangle((GameWidth - marryHerTexture2D.Bounds.Width) / 2, 300, marryHerTexture2D.Bounds.Width, marryHerTexture2D.Bounds.Height);

            //加载双喜图片纹理和初始化位置大小
            doubleHappinessTexture2D = Content.Load<Texture2D>("DoubleHappiness");
            doubleHappinessRectangle = new Rectangle((GameWidth - doubleHappinessTexture2D.Bounds.Width) / 2, 330, doubleHappinessTexture2D.Bounds.Width, doubleHappinessTexture2D.Bounds.Height);

            //加载离婚图片纹理和初始化位置大小
            divorceTexture2D = Content.Load<Texture2D>("Divorce");
            divorceRectangle = new Rectangle(50, 640, divorceTexture2D.Bounds.Width, divorceTexture2D.Bounds.Height);
            //加载主菜单图片纹理和初始化位置大小
            mainMenuTexture2D = Content.Load<Texture2D>("MainMenu");
            mainMenuRectangle = new Rectangle(280, 640, mainMenuTexture2D.Bounds.Width, mainMenuTexture2D.Bounds.Height);

            //加载结婚进行曲
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

                    //修改自定义触摸位置的X，Y轴坐标为鼠标当前X,Y位置
                    mouseCursorRectangle.X = (int)X;
                    mouseCursorRectangle.Y = (int)Y;

                    //判断当前场景是否是主菜单
                    if (currentScence == GameScence.MainMenu)
                    {
                        //判断是否点击了关于按钮
                        if ((aboutRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y)))
                        {
                            //将当前场景切换关于场景
                            currentScence = GameScence.About;
                            //按钮点击音效
                            if (!isMuted)
                            {
                                click.Play();
                            }
                        }
                        //判断是否点击关闭音效音乐按钮
                        if ( soundOnRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y))
                        {
                            isMuted = !isMuted;
                            MediaPlayer.IsMuted = isMuted;
                            //按钮点击音效
                            if (!isMuted)
                            {
                                click.Play();
                            }
                        }
                        //判断是否点击了开始游戏按钮
                        if ( playNormalRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y))
                        {
                            //跳转到游戏场景
                            currentScence = GameScence.GamePlaying;

                            starting = true;
                            //按钮点击音效
                            if (!isMuted)
                            {
             
                                click.Play();
                            }


                        }

                    }

                    //判断当前场景是否是游戏场景
                    else if (currentScence == GameScence.GamePlaying)
                    {
                        //判断是否点击了关于按钮
                        if (backRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y))
                        {
                            //将当前场景切换到主菜单
                            currentScence = GameScence.MainMenu;
                            //按钮点击音效
                            if (!isMuted)
                            {
                                click.Play();
                            }

                        }

                        //判断是否点击了选老婆按钮
                        if (marryHerRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y))
                        {
                            //停止切换图片
                            starting = false;
                            //切换到游戏结束页面
                            currentScence = GameScence.GameOver;
                            if (!isMuted)
                            {
                                //停止之前的游戏音乐
                                MediaPlayer.Stop();
                                //播放结婚进行曲
                                MediaPlayer.Play(weddingMarch);
                            }
                            //按钮点击音效
                            if (!isMuted)
                            {
                                click.Play();
                            }

                        }

                       

                    }

                    //判断当前场景是否是关于
                    else if (currentScence == GameScence.About)
                    {
                        //判断是否点击了关于按钮
                        if ( backRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y))
                        {
                            //将当前场景切换到主菜单
                            currentScence = GameScence.MainMenu;
                            //按钮点击音效
                            if (!isMuted)
                            {
                                click.Play();
                            }
                        }

                    }
                    else if (currentScence == GameScence.GameOver)
                    {

                        //判断是否点击了离婚
                        if ( divorceRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y))
                        {
                            //停止切换图片
                            starting = true;
                            //切换到游戏页面
                            currentScence = GameScence.GamePlaying;
                            if (!isMuted)
                            {
                                //停止之前的结婚进行曲
                                MediaPlayer.Stop();
                                //播放结婚进行曲
                                MediaPlayer.Play(music);
                            }
                            //按钮点击音效
                            if (!isMuted)
                            {
                                click.Play();
                            }
                        }
                        //判断是否点击了主菜单
                        if ( mainMenuRectangle.Contains(mouseCursorRectangle.X, mouseCursorRectangle.Y))
                        {
                            //切换到游戏页面
                            currentScence = GameScence.MainMenu;

                            if (!isMuted)
                            {
                                //停止之前的结婚进行曲
                                MediaPlayer.Stop();
                                //播放结婚进行曲
                                MediaPlayer.Play(music);
                            }
                            //按钮点击音效
                            if (!isMuted)
                            {
                                click.Play();
                            }
                        }

                    }
                    //鼠标状态赋值给前鼠标状态
                    //prevMouseState = mouseState;
                    mouseCursorRectangle = new Rectangle();



                }

            }

            if (starting)
            {
                timeLastFrame = timeLastFrame + gameTime.ElapsedGameTime.Milliseconds;//图片切换后经过的时间（毫秒）
                if (timeLastFrame > timePerFame)
                {
                    timeLastFrame = timeLastFrame - timePerFame;//将图片切换后经过的时间恢复到小于每秒切换时间，保证下面代码执行一次
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

            ////获取当前鼠标状态
            //MouseState mouseState = Mouse.GetState();
            ////修改自定义鼠标位置的X，Y轴坐标为鼠标当前X,Y位置
            //mouseCursorRectangle.X = mouseState.X;
            //mouseCursorRectangle.Y = mouseState.Y;

            ////判断当前场景是否是主菜单
            //if (currentScence == GameScence.MainMenu)
            //{
            //    //判断是否点击了关于按钮
            //    if ((mouseState != prevMouseState && mouseState.LeftButton != ButtonState.Released && aboutRectangle.Contains(mouseState.X, mouseState.Y))||)
            //    {
            //        //将当前场景切换关于场景
            //        currentScence = GameScence.About;
            //        //按钮点击音效
            //        if(!isMuted)
            //        {
            //            click.Play();
            //        }
            //    }
            //    //判断是否点击关闭音效音乐按钮
            //    if (mouseState != prevMouseState && mouseState.LeftButton != ButtonState.Released && soundOnRectangle.Contains(mouseState.X, mouseState.Y))
            //    {
            //        isMuted =! isMuted;
            //        MediaPlayer.IsMuted = isMuted;
            //        //按钮点击音效
            //        if (!isMuted)
            //        {
            //            click.Play();
            //        }
            //    }
            //    //判断是否点击了开始游戏按钮
            //    if (mouseState != prevMouseState && mouseState.LeftButton != ButtonState.Released && playNormalRectangle.Contains(mouseState.X, mouseState.Y))
            //    {
            //        //跳转到游戏场景
            //        currentScence = GameScence.GamePlaying;

            //        starting = true;
            //        //按钮点击音效
            //        if (!isMuted)
            //        {
            //            click.Play();
            //        }


            //    }

            //}

            ////判断当前场景是否是游戏场景
            //else if (currentScence == GameScence.GamePlaying)
            //{
            //    //判断是否点击了关于按钮
            //    if (mouseState != prevMouseState && mouseState.LeftButton != ButtonState.Released && backRectangle.Contains(mouseState.X, mouseState.Y))
            //    {
            //        //将当前场景切换到主菜单
            //        currentScence = GameScence.MainMenu;
            //        //按钮点击音效
            //        if (!isMuted)
            //        {
            //            click.Play();
            //        }

            //    }

            //    //判断是否点击了选老婆按钮
            //    if (mouseState!=prevMouseState&& mouseState.LeftButton != ButtonState.Released && marryHerRectangle.Contains(mouseState.X, mouseState.Y))
            //    {
            //        //停止切换图片
            //        starting = false;
            //        //切换到游戏结束页面
            //        currentScence = GameScence.GameOver;
            //        if (!isMuted)
            //        {
            //            //停止之前的游戏音乐
            //            MediaPlayer.Stop();
            //            //播放结婚进行曲
            //            MediaPlayer.Play(weddingMarch);
            //        }
            //        //按钮点击音效
            //        if (!isMuted)
            //        {
            //            click.Play();
            //        }

            //    }

            //    if (starting)
            //    {
            //        timeLastFrame = timeLastFrame + gameTime.ElapsedGameTime.Milliseconds;//图片切换后经过的时间（毫秒）
            //        if (timeLastFrame > timePerFame)
            //        {
            //            timeLastFrame = timeLastFrame - timePerFame;//将图片切换后经过的时间恢复到小于每秒切换时间，保证下面代码执行一次
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

            ////判断当前场景是否是关于
            //else if(currentScence == GameScence.About)
            //{
            //    //判断是否点击了关于按钮
            //    if (mouseState != prevMouseState && mouseState.LeftButton != ButtonState.Released && backRectangle.Contains(mouseState.X, mouseState.Y))
            //    {
            //        //将当前场景切换到主菜单
            //        currentScence = GameScence.MainMenu;
            //        //按钮点击音效
            //        if (!isMuted)
            //        {
            //            click.Play();
            //        }
            //    }

            //}
            //else if (currentScence == GameScence.GameOver)
            //{

            //    //判断是否点击了离婚
            //    if (mouseState != prevMouseState && mouseState.LeftButton != ButtonState.Released && divorceRectangle.Contains(mouseState.X, mouseState.Y))
            //    {
            //        //停止切换图片
            //        starting = true;
            //        //切换到游戏页面
            //        currentScence = GameScence.GamePlaying;
            //        if (!isMuted)
            //        {
            //            //停止之前的结婚进行曲
            //            MediaPlayer.Stop();
            //            //播放结婚进行曲
            //            MediaPlayer.Play(music);
            //        }
            //        //按钮点击音效
            //        if (!isMuted)
            //        {
            //            click.Play();
            //        }
            //    }
            //    //判断是否点击了主菜单
            //    if (mouseState != prevMouseState && mouseState.LeftButton != ButtonState.Released && mainMenuRectangle.Contains(mouseState.X, mouseState.Y))
            //    {
            //        //切换到游戏页面
            //        currentScence = GameScence.MainMenu;

            //        if (!isMuted)
            //        {
            //            //停止之前的结婚进行曲
            //            MediaPlayer.Stop();
            //            //播放结婚进行曲
            //            MediaPlayer.Play(music);
            //        }
            //        //按钮点击音效
            //        if (!isMuted)
            //        {
            //            click.Play();
            //        }
            //    }

            //}
            ////鼠标状态赋值给前鼠标状态
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

            //绘制游戏背景
            spriteBatch.Begin();
            spriteBatch.Draw(gameBackgroundTexture2D, gameBackgroundRectangle, Color.White);
            spriteBatch.End();

            if (currentScence == GameScence.MainMenu)
            {

                spriteBatch.Begin();
                //主菜单场景绘制界面
                spriteBatch.Draw(gameTitleTexture2D, gameTitleRectangle, Color.White);
                //绘制开始按钮
                spriteBatch.Draw(playNormalTexture2D, playNormalRectangle, Color.White);

                //绘制声效开关和关于按钮
                if (isMuted)
                {
                    //静音
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
                //游戏场景绘制界面
                //绘制返回按钮
                spriteBatch.Draw(backTexture2D, backRectangle, Color.White);

                //绘制老婆图象
                spriteBatch.Draw(wifeTextureList[currentFrame], wifeListRectangle, Color.White);

                //绘制选老婆按钮
                spriteBatch.Draw(marryHerTexture2D, marryHerRectangle, Color.White);

                spriteBatch.End();
            }
            else if (currentScence == GameScence.GameOver)
            {

                spriteBatch.Begin();
                //游戏结束绘制界面
                //绘制老婆图象
                spriteBatch.Draw(wifeTextureList[currentFrame], wifeListRectangle, Color.White);
                //绘制双喜图片
                spriteBatch.Draw(doubleHappinessTexture2D, doubleHappinessRectangle, Color.White);
                //绘制离婚按钮
                spriteBatch.Draw(divorceTexture2D, divorceRectangle, Color.White);
                //绘制主菜单按钮
                spriteBatch.Draw(mainMenuTexture2D, mainMenuRectangle, Color.White);
                spriteBatch.End();
            }
            else if (currentScence == GameScence.About)
            {

                spriteBatch.Begin();
                //关于场景绘制界面
                spriteBatch.DrawString(aboutFont, aboutText, aboutFontPosition, Color.Orange);
                //绘制返回按钮
                spriteBatch.Draw(backTexture2D, backRectangle, Color.White);
                spriteBatch.End();
            }

            spriteBatch.Begin();
            //绘制自定义鼠标
            spriteBatch.Draw(mouseCursorTexture2D, mouseCursorRectangle, Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

    }
}
