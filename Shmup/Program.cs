using System;
using System.Drawing;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;
using SdlDotNet.Core;

using System.Collections.Generic;
using System.IO;

namespace Shmup {

    // ===== Data Types =====

    public class SpriteSheet {
        
        private string imagefile;
        
        private List<Rectangle> rects = new List<Rectangle>();

        private SpriteSheet() {
        }

        public static SpriteSheet load(string configfile) {
            SpriteSheet ss = new SpriteSheet();
            StreamReader sr = new StreamReader(configfile);
            string line = sr.ReadLine();
            line = sr.ReadLine();
            string[] fields = line.Split('=');
            ss.imagefile = fields[1];
            while (!sr.EndOfStream) {
                line = sr.ReadLine();
                fields = line.Split(',');
                int x = int.Parse(fields[1]);
                int y = int.Parse(fields[2]);
                int w = int.Parse(fields[3]);
                int h = int.Parse(fields[4]);
                Rectangle r = new Rectangle(x,y,w,h);
                ss.rects.Add(r);
            }
            return ss;
        }

        public Rectangle getRectangle(int sprite) {
            return rects[sprite];
        }

        public string getImagefile() {
            return imagefile;
        }
    }

    public class Ship {

        // state

        private int x;
        private int y;
        private int dx;
        private int dy;
        private int sprite;
        private int direction;
        private int rotation;

        // constructors

        public Ship(int x, int y, int sprite, int direction, int rotation, int dx, int dy) {
            this.x = x;
            this.y = y;
            this.sprite = sprite;
            this.direction = direction;
            this.rotation = rotation;
        }

        // behaviour 

        public void setX(int x) {
            this.x = x;
        }

        public int getX() {
            return x;
        }

        public void setY(int y) {
            this.y = y;
        }

        public int getY() {
            return y;
        }

        public void setSprite(int sprite)  {
            this.sprite = sprite;
        }

        public int getSprite() {
            return sprite;
        }

        public void setDirection(int direction) {
            this.direction = direction;
        }

        public int getDirection() {
            return direction;
        }

        public void setRotation(int rotation) {
            this.rotation = rotation;
        }

        public int getRotation() {
            return rotation;
        }

        public void move() {
            x += dx;
            y += dy;

            if (rotation != 0) {
                direction = (direction + rotation) % 360;
            }
        }

        public void thrust(int thrust) {
            double rad = (double)(direction + 90) * Math.PI / 180.0;
            dx += (int)((double)thrust * Math.Cos(rad));
            dy += -(int)((double)thrust * Math.Sin(rad));
        }

    }

    public class Asteroid {

        // state

        private int x;
        private int y;
        private int dx;
        private int dy;
        private int sprite;
        private int direction;
        private int rotation;

        // constructors

        public Asteroid(int x, int y, int sprite, int direction, int rotation, int dx, int dy) {
            this.x = x;
            this.y = y;
            this.dx = dx;
            this.dy = dy;
            this.sprite = sprite;
            this.direction = direction;
            this.rotation = rotation;
        }

        // behaviour 

        public void setX(int x) {
            this.x = x;
        }

        public int getX() {
            return x;
        }

        public void setY(int y) {
            this.y = y;
        }

        public int getY() {
            return y;
        }

        public void setSprite(int sprite) {
            this.sprite = sprite;
        }

        public int getSprite() {
            return sprite;
        }

        public void setDirection(int direction) {
            this.direction = direction;
        }

        public int getDirection() {
            return direction;
        }

        public void setRotation(int rotation) {
            this.rotation = rotation;
        }

        public int getRotation() {
            return rotation;
        }

        public void move() {
            x += dx;
            y += dy;

            if (rotation != 0) {
                direction = (direction + rotation) % 360;
            }
        }

    }

    // -- Laser --

    public class Laser {

        private int sprite;
        private Beam beam;

        public Laser(int sprite, double x, double y, double velocity, double direction) {
            this.sprite = sprite;
            this.beam = new Beam(x,y,velocity,direction);
        }

        public int getSprite() {
            return sprite;
        }

        public Beam getBeam() {
            return beam;
        }

    }


    // -- Beam --

    public class Beam {

        private double mass;
        private Position position;
        private Motion motion;

        public Beam(double x, double y, double velocity, double direciton) {
            this.position = new Position(x,y);
            this.motion = new Motion(velocity,direciton);
        }

        public Position getPosition() {
            return position;
        }

        public Motion getMotion() {
            return motion;
        }

    }
    
    // -- Body --

    public class Body {

        private double mass;
        private Position position;
        private Motion motion;

        public Body(double mass, double x, double y, double velocity, double direciton) {
            this.mass = mass;
            this.position = new Position(x,y);
            this.motion = new Motion(velocity,direciton);
        }

        public double getMass() {
            return mass;
        }

        public void setMass(double mass) {
            this.mass = mass;
        }

        public Position getPosition() {
            return position;
        }

        public Motion getMotion() {
            return motion;
        }

    }

    // -- Position --

    public class Position {

        private double x;
        private double y;

        public Position(double x, double y) {
            this.x = x;
            this.y = y;
        }

        public double getX() {
            return x;
        }

        public void setX(double x) {
            this.x = x;
        }

        public double getY() {
            return y;
        }

        public void setY(double y) {
            this.y = y;
        }

    }

    // -- Motion --

    public class Motion {

        private double velocity;
        private double direction;

        public Motion(double velocity, double direction) {
            this.velocity = velocity;
            this.direction = direction;
        }

        public double getVelocity() {
            return velocity;
        }

        public void setVelocity(double velocity) {
            this.velocity = velocity;
        }

        public double getDirection() {
            return direction;
        }

        public void setDirection(double direction) {
            this.direction = direction;
        }

    }

    // -- 2D Netwtonian Physics Model --

    public class Newtonian {

        private Newtonian() {
        }

        public static void accelerate(Body body, double velocity, double direction) {
            // Calculate the effect of the acceleration on the body.
            // Update the motion of the body.
        }

        public static void collide(Body bodyA, Body bodyB) {
            // Calculate the effect of two, non-deforming spherical bodies, colliding.
            // Update the motion of each body.
        }

        public static void attract(Body body, Point point, double force) {
            // Calculate the effect of a force of attraction on a body.
            // Update the motion of the body.
        }

        public static void attract(Beam beam, Point point, double force) {
            // Calculate the effect of a force of attraction on a beam.
            // Update the motion of the beam.
        }

        public static void move(Position position, Motion motion) {
            // Calcluate the new position given the motion.
            // Update the position.
            double rad = motion.getDirection() * Math.PI / 180.0;
            double dx = motion.getVelocity() * Math.Cos(rad);
            double dy = motion.getVelocity() * Math.Sin(rad) * -1.0;
            position.setX(position.getX() + dx);
            position.setY(position.getY() + dy);
        }

    }

    // -- Program, Entry Point --

    static class Program {

        // STATE
        // Keep the state of the elements of the game here (variables hold state).

        static Random rnd = new Random();

        static SpriteSheet ss;
        static Ship ship;
        static Asteroid[] asteroids = new Asteroid[2];
        static Laser laser;

        // This procedure is called (invoked) before the first time onTick is called.
        static void onInit() {
            ship = new Ship(FRAME_WIDTH/2, FRAME_HEIGHT/2, 0, 0, 0, 0, 0);
            asteroids[0] = new Asteroid(rnd.Next(FRAME_WIDTH),rnd.Next(FRAME_HEIGHT), 1, rnd.Next(360), rnd.Next(4), rnd.Next(4), rnd.Next(4));
            asteroids[1] = new Asteroid(rnd.Next(FRAME_WIDTH), rnd.Next(FRAME_HEIGHT), 2, rnd.Next(360), rnd.Next(4), rnd.Next(4), rnd.Next(4));
        }

        // This procedure is called (invoked) for each window refresh
        static void onTick(object sender, TickEventArgs args) {

            // STEP
            // Update the automagic elements and enforce the rules of the game here.

            ship.move();

            if (ship.getX() < 0) {
                ship.setX(FRAME_WIDTH);
            } else if (ship.getX() > FRAME_WIDTH) {
                ship.setX(0);
            }

            if (ship.getY() < 0) {
                ship.setY(FRAME_HEIGHT);
            } else if (ship.getY() > FRAME_HEIGHT) {
                ship.setY(0);
            }

            for (int i = 0; i < asteroids.Length; ++i) { 
                asteroids[i].move();

                if (asteroids[i].getX() < 0) {
                    asteroids[i].setX(FRAME_WIDTH);
                } else if (asteroids[i].getX() > FRAME_WIDTH) {
                    asteroids[i].setX(0);
                }

                if (asteroids[i].getY() < 0) {
                    asteroids[i].setY(FRAME_HEIGHT);
                } else if (asteroids[i].getY() > FRAME_HEIGHT) {
                    asteroids[i].setY(0);
                }
            }

            if (laser != null) {
                Beam beam = laser.getBeam();
                Newtonian.move(beam.getPosition(),beam.getMotion());
                if (   (beam.getPosition().getX() < 0) 
                    || (beam.getPosition().getX() > FRAME_WIDTH)
                    || (beam.getPosition().getY() < 0)
                    || (beam.getPosition().getY() > FRAME_HEIGHT)) {
                    laser = null;
                } 
            }


            // DRAW
            // Draw the new view of the game based on the state of the elements here.

            drawBackground();

            if (laser != null) { 
                drawSprite(laser.getSprite(),(int)laser.getBeam().getPosition().getX(), (int)laser.getBeam().getPosition().getY(), (int)laser.getBeam().getMotion().getDirection()+90);
            }

            drawSprite(ship.getSprite(),ship.getX(),ship.getY(),ship.getDirection());

            for (int i = 0; i < asteroids.Length; ++i) {
                drawSprite(asteroids[i].getSprite(), asteroids[i].getX(), asteroids[i].getY(), asteroids[i].getDirection());
            }


            // ANIMATE 
            // Step the animation frames ready for the next tick
            // ...

            // REFRESH
            // Tranfer the new view to the screen for the user to see.
            video.Update();

        }

        // this procedure is called when the mouse is moved
        static void onMouseMove(object sender, SdlDotNet.Input.MouseMotionEventArgs args) {
        }

        // this procedure is called when a mouse button is pressed or released
        static void onMouseButton(object sender, SdlDotNet.Input.MouseButtonEventArgs args) {
        }

        // this procedure is called when a key is pressed or released
        static void onKeyboard(object sender, SdlDotNet.Input.KeyboardEventArgs args) {

            if (args.Down) { 

                switch (args.Key) {
                    case SdlDotNet.Input.Key.RightArrow :
                        ship.setRotation(-12);
                        break;
                    case SdlDotNet.Input.Key.LeftArrow :
                        ship.setRotation(+12);
                        break;
                    case SdlDotNet.Input.Key.UpArrow :
                        ship.thrust(2);
                        break;
                    case SdlDotNet.Input.Key.Space :
                        if (laser == null) {
                            laser = new Laser(3,ship.getX(),ship.getY(),12.0,ship.getDirection()+90);
                        }
                        break;
                        
                    case SdlDotNet.Input.Key.Escape :
                        Events.QuitApplication();
                        break;
                }

            } else {

                switch (args.Key) {
                    case SdlDotNet.Input.Key.RightArrow :
                    case SdlDotNet.Input.Key.LeftArrow :
                        ship.setRotation(0);
                        break;
                }

            }

        }


        // --------------------------
        // ----- GAME Utilities -----  
        // --------------------------

        // draw the background image onto the frame
        static void drawBackground() {
            video.Blit(background);
        }

        // draw the sprite image onto the frame
        // Sprite sprite - which sprite to draw
        // int x, int y - the co-ordinates of where to draw the sprite on the frame.
        static void drawSprite(int sprite, int x, int y, int direction) {
            Surface temp1 = sprites.CreateSurfaceFromClipRectangle(ss.getRectangle(sprite));
            Surface temp2 = temp1.CreateRotatedSurface(direction,false);
            Surface temp3 = temp2.Convert(video,false,false);
            temp3.SourceColorKey = temp3.GetPixel(new Point(0, 0));
            video.Blit(temp3, new Point(x - (temp3.Width / 2), y - (temp3.Height / 2)));
            temp3.Dispose();
            temp2.Dispose();
            temp1.Dispose();
        }

        // -- APPLICATION ENTRY POINT --

        static void Main() {
            //System.Windows.Forms.Cursor.Hide();
            ss = SpriteSheet.load("images/config.csv");

            // Create display surface.
            video = Video.SetVideoMode(FRAME_WIDTH, FRAME_HEIGHT, COLOUR_DEPTH, FRAME_RESIZABLE, USE_OPENGL, FRAME_FULLSCREEN, USE_HARDWARE);
            Video.WindowCaption = "Shmup";
            Video.WindowIcon(new Icon(@"images/shmup.ico"));

            // invoke application initialisation subroutine
            setup();

            // invoke secondary initialisation subroutine
            onInit();

            // register event handler subroutines
            Events.Tick += new EventHandler<TickEventArgs>(onTick);
            Events.Quit += new EventHandler<QuitEventArgs>(onQuit);
            Events.KeyboardDown += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(onKeyboard);
            Events.KeyboardUp += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(onKeyboard);
            Events.MouseButtonDown += new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(onMouseButton);
            Events.MouseButtonUp += new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(onMouseButton);
            Events.MouseMotion += new EventHandler<SdlDotNet.Input.MouseMotionEventArgs>(onMouseMove);

            // while not quit do process events
            Events.TargetFps = 60;
            Events.Run();
        }

        // This procedure is called after the video has been initialised but before any events have been processed.
        static void setup() {

            // Load Art
            sprites = new Surface(ss.getImagefile());

            backgroundColour = sprites.GetPixel(new Point(0, 0));

            background = video.CreateCompatibleSurface();
            background.Transparent = false;
            background.Fill(backgroundColour);

        }

        // This procedure is called when the event loop receives an exit event (window close button is pressed)
        static void onQuit(object sender, QuitEventArgs args) {
            Events.QuitApplication();
        }

        // -- DATA --

        const int FRAME_WIDTH = 800;
        const int FRAME_HEIGHT = 600;
        const int COLOUR_DEPTH = 32;
        const bool FRAME_RESIZABLE = false;
        const bool FRAME_FULLSCREEN = false;
        const bool USE_OPENGL = false;
        const bool USE_HARDWARE = true;

        static Surface video;  // the window on the display

        static Surface background;
        static Surface sprites;

        static Color backgroundColour;

    }
}
