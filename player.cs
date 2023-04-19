using Godot;
using System;
using System.IO.IsolatedStorage;

public partial class player : CharacterBody2D
{
    // Movement variables, can be set in the Inspector
    // Movement speed
    [Export]
    public int walkSpeed = 400;
    [Export]
    public int sprintSpeed = 700;
    // Force of jump
    [Export]
    public int jumpForce = 1500;
    // "Gravity"
    [Export]
    public int gravity = 4000;

    // Motion for player, will later be changed into built-in Velocity
    private Vector2 motion;
    // When isJumping is false, you aren't jumping
    private bool isJumping;
    // When jumpCD is true, you can't jump
    private bool jumpCD;
    // bool for facing left
    private bool faceLeft;
    // bool for being alive
    private bool isAlive;

    // When jump timer runs out
    public void OnJumpTimerTimeout()
    {
        isJumping = false;
    }

    // On startup
    public override void _Ready()
    {
        jumpCD = false;
        faceLeft = false;
    }

    // Handling all general processes
    public override void _Process(double delta)
    {
        // Animate Sprite
        HandleAnimations();
    }


    // Handling movement and physics
    public override void _PhysicsProcess(double delta)
    {
        // Rotation is always 0 degrees
        Rotation = 0;

        if (IsOnFloor())
        {
            HandleHorizontalGroundMovement();
        } else if (Input.IsActionPressed("walk_left") || Input.IsActionPressed("walk_right"))
        {
            HandleHorizontalAirMovement();
        }

        HandleJumping(delta);

        // Setting built-in Velocity to motion
        Velocity = motion;

        // Moving the player (using built-in Velocity)
        MoveAndSlide();

    } // End _PhysicsProcess()

    private void HandleHorizontalGroundMovement()
    {
        bool isWalking = false;
        bool isSprinting = false;

        // Init motion to zero
        motion = Vector2.Zero;

        // Take input and change ground motion accordingly
        if (Input.IsActionPressed("walk_left") || Input.IsActionPressed("walk_right")) // If walking
        {
            isWalking = true;
        }
        if (Input.IsActionPressed("sprint")) // If sprinting
        {
            isSprinting = true;
            isWalking = false;
        }
        
        if (Input.IsActionPressed("walk_left"))
        {
            motion.X -= 1;
        }
        else if (Input.IsActionPressed("walk_right"))
        {
            motion.X += 1;
        }
        
        if (isWalking)
        {
            motion.X = motion.X * walkSpeed;
        }
        else if (isSprinting) {
            motion.X = motion.X * sprintSpeed;
        }
    }// End HandleHorizontalGroundMovement()

    private void HandleJumping(double delta)
    {
        // Ceiling for motion.Y * JumpForce
        int jumpCeil = -1050;

        // If not jumping currently
        if (!isJumping)
        {
            // Releasing space button allows jumping again
            if (Input.IsActionJustReleased("jump"))
            {
                jumpCD = false;
            }
            else if (Input.IsActionPressed("jump") && IsOnFloor() && !jumpCD)
            {
                // Jumping
                isJumping = true;
                jumpCD = true;
                // Start JumpTimer
                GetNode<Timer>("JumpTimer").Start();
                // Go up fast
                motion.Y = (jumpForce * (float)delta) * -23;
            }
            // Else you can't jump
            else
            {
                // Gravity is applied
                motion.Y += (gravity * (float)delta) * 2;
            }
        }

        // If jumping
        if (isJumping)
        {
            // If jump is pressed and motion.Y is above jumpCeil velocity
            if (Input.IsActionPressed("jump") && motion.Y > jumpCeil)
            {
                // Go up
                motion.Y -= jumpForce * (float)delta;
            }
            // Else if jump is released
            else if (Input.IsActionJustReleased("jump"))
            {
                // Turn off jumping
                isJumping = false;
                // jumpCD off
                jumpCD = false;
            }
            // Else jumping and below jumpCeil
            else
            {
                // Turn off jumping
                isJumping = false;
                // Gravity is applied
                motion.Y += (gravity * (float)delta) * 2;
            }
        }// End if (isJumping)
    }// End HandleJumping()

    private void HandleHorizontalAirMovement()
    {
        // Horizontal speed in air
        int airWalkSpeed = 200;

        // If not moving
        if (motion.X == 0)
        {
            // And the player hits "walk left"
            if (Input.IsActionPressed("walk_left"))
            {
                motion.X = 0;
                // Move left at half speed
                motion.X -= airWalkSpeed;
            }
            else if (Input.IsActionPressed("sprint"))
            {
                if (Input.IsKeyPressed(Key.A))
                {
                    motion.X = 0;
                    // Move left at half speed
                    motion.X -= airWalkSpeed;
                }
            }

            if (Input.IsActionPressed("walk_right"))
            {
                motion.X = 0;
                // Move right at half speed
                motion.X += airWalkSpeed;
            }
            else if (Input.IsActionPressed("sprint"))
            {
                if (Input.IsKeyPressed(Key.D))
                {
                    motion.X = 0;
                    // Move left at half speed
                    motion.X += airWalkSpeed;
                }
            }
        }

        // If moving right
        if (motion.X > 0)
        {
            // And the player hits "walk left"
            if (Input.IsActionPressed("walk_left"))
            {
                motion.X = 0;
                // Move left at half speed
                motion.X -= airWalkSpeed;
            }
            else if (Input.IsActionPressed("sprint"))
            {
                if (Input.IsKeyPressed(Key.A))
                {
                    motion.X = 0;
                    // Move left at half speed
                    motion.X -= airWalkSpeed;
                }
            }
        }
        // If moving left
        else if (motion.X < 0)
        {   // And the player hits "walk right"
            if (Input.IsActionPressed("walk_right"))
            {
                motion.X = 0;
                // Move right at half speed
                motion.X += airWalkSpeed;
            }
            else if (Input.IsActionPressed("sprint"))
            {
                if (Input.IsKeyPressed(Key.D))
                {
                    motion.X = 0;
                    // Move left at half speed
                    motion.X += airWalkSpeed;
                }
            }
        }
    }// End HandleHorizontalAirMovement()

    private void HandleAnimations()
    {
        // Getting the AnimatedSprite2D
        var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        // Playing the animatedSprite2D
        animatedSprite2D.Play();
        // Resetting the speedScale
        animatedSprite2D.SpeedScale = 1;

        // Checking which direction player is facing
        if (Input.IsKeyPressed(Key.A))
        {
            faceLeft = true;
        }
        else if (Input.IsKeyPressed(Key.D))
        {
            faceLeft = false;
        }

        // Flipping animations if facing left
        animatedSprite2D.FlipH = faceLeft;

        // Check which animation to play
        if (!IsOnFloor() && motion.Y < 0)
        {
            animatedSprite2D.Animation = "jump";
        }
        else if(!IsOnFloor() && motion.Y > 0)
        {
            animatedSprite2D.Animation = "fall";
        }
        else if (Input.IsActionPressed("sprint") && motion.X != 0)
        {
            // Playing sprint animation
            animatedSprite2D.Animation = "sprint";
        }
        else if (Input.IsActionPressed("walk_left") || (Input.IsActionPressed("walk_right")))
        {
            // Slowing the animation speed
            animatedSprite2D.SpeedScale = 0.8F;
            // Playing sprint animation
            animatedSprite2D.Animation = "sprint";
        }
        else
        {
            // Playing idle animation
            animatedSprite2D.Animation = "idle";
        }
    }// End HandleAnimations()

}// End player class
