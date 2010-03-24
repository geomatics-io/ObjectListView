.. -*- coding: UTF-8 -*-

:Subtitle: Pretty distractions

.. _animations:

Animation-envy
==============

I have to admit that I suffer from Google envy. It's a fairly serious condition. I can be working
normally and then I see something that provokes my condition. The feeling of envy seizes me, I grit my
teeth in annoyance, and I reach for my paper bag to keep calm.

In my self defense, the source of my envy is nothing as crass as their billion dollar development budget,
their wonderful working conditions, or their thousand plus dollar share price. All these things fall into
insignificance in comparison with the true object of my obsession: their animations! The way they
effortlessly add little spinning stars, glowing text or fading sparkles to their applications. My
applications sit there obediently, static and passive, lacking the moving eye candy that make Google apps
cute and cool. They lack pazzaz and sparkle. 

But no more! For fellow sufferers of animation-envy, I present the "Sparkle" animation framework. With
this framework, my applications (and yours too) can spin, twirl and dazzle. 

Once more, at little more sedately
----------------------------------

OK, what does it really do? 

The Sparkle animation library lets you draw animations over the top of other `Controls.`
Traditionally, this would be a `UserControl` constructed just for the purpose of showing
an animation. But the Sparkle library lets you draw animation over the top of (almost) any
`Control.`

To avoid wasting your time, you should know:

1. This framework is designed to present eye-candy, do-nothing pretty things. The animations are not
interactive. You cannot click on things. It doesn't do collision detection. They just look swish.

2. For a control to show an animation, that control must use the `Paint` event. 
In WinForms, this includes `Panel`, `Button`, `Label`,
`PictureBox`, `UserControl`, numeric spin controls, and (oddly enough) `DataGridView`.
Two notable exceptions are the `ListView` and `TreeView` controls,
which do not usefully support that event. There is little you can do about the `TreeView`, but
to draw animations on a `ListView` you can use the ObjectListView_ project.

.. _ObjectListView: http://www.codeproject.com/KB/list/ObjectListView.aspx


Concepts
--------

There are four major "things" in the Sparkle library:

1. Animations. An animation is the canvas upon which sprite are placed. It is the white board upon
which things are drawn.

2. Sprites. Sprites are things that can be drawn. There are several flavours of sprites -- one for
images, another for text, still another for shapes. It is normal to make your own types of sprites
by subclassing `Sprite` (or implementing `ISprite`).

3. Effects. Effects are things that make changes to sprites over time. They are the "movers and shakers"
in the library, who actually do things. Sprites sit there, completely passive, looking pretty,
but the effects push them around, change their visibility, spin or size.

4. Locators. Locators are things that known how to calculate a point or a rectangle. Rather than saying
"Put this sprite at (10, 20)," they allow you to say "Put this sprite at the bottom right corner
of this other sprite." This idea can be tricky to get your mind around, but once you have grasped it,
it is powerful.

One final "thing" about the library is that the Sparkle library is declarative. You say what you want the animation to do, and then you run the
animation. The animation is completely defined before it begins.


Simple example
--------------

OK, ok, just show me the code!

Let's start with a very simple example. Let's animate the word "Sparkle" 
so it moves from the top left of the control to the bottom right.

First, we make an animation on the `UserControl` that we've made to hold the animation::

    AnimationAdapter adapter = new AnimationAdapter(this.userControl1);
    Animation animation = adapter.Animation;

`AnimationAdapter` is a simple class that gives animation ability to an existing control.
Remember, the `Animation` is the canvas on which the sprites will do their stuff.

Once we have an animation, we need some sprites to put on it. For this example,
we'll make a `TextSprite`, which will show the word "Sparkle"::

    TextSprite sparkle = new TextSprite("Sparkle!", new Font("Gill Sans", 48), Color.Blue);

OK, we have our sprite. What do we want it to do? Whenever we think about a Sprite "doing" something,
we know we need an `Effect.`

We want to move it to the centre of the animation.
Moving (or any other sort of change over time) requires an `Effect`. In this case, we need a `MoveEffect.`
You can create these directly -- using `new MoveEffect(...)` -- or you can use the `Effects` factory,
which has lots methods to make `Effect` objects::

    sparkle.Add(100, 1000, Effects.Move(Corner.TopLeft, Corner.BottomRight));

This says, "Beginning 100 milliseconds after the sprite starts, and lasting for 1000 milliseconds,
move this sprite from its initial location to the middle of the animation."

That's all we want the sprite to do at the moment, so now we add the sprite to the animation.
Not all sprites are active at the beginning of the animation, so when we add the sprite the animation
we also tell it when the sprite should begin. In this case, we *do* want the sprite to start when
the animation starts, so we give `0` as the start time for the sprite::

    animation.Add(0, sparkle);

And finally, we tell the animation to run::

    animation.Start();

A little bit more interesting
-----------------------------

For each thing you want in the animation:
    * Make a sprite.
    * Give it effects to make it do what you want
    * Add the sprite to the animation


OK. It's not particularly impressive.

The animation framework is independent of all UI controls. If you know what you are doing, you can
work it into any UI framework.

With WPF, the situation is easier.

Control-independent
-------------------

The animation framework is independent of any UI. The animation is configured, started. When it is ready
to be updated, it triggers an event. The UI listens for this event and tells the animation to draw
itself into a graphics context. For example, if we want to put an animation onto the `userControl1`
we would do something like this::

    void RunAnimation() {
        this.animation = new Animation();
        this.animation.Redraw += new EventHandler<EventArgs>(Animation_Redraw);
        userControl1.Paint += new PaintEventHandler(UserControl1_Paint);

        // Configure the animation here

        animation.Start();
    }
    Animation animation;

    void Animation_Redraw(object sender, EventArgs e) {
        this.userControl1.Invalidate();
    }

    void UserControl1_Paint(object sender, PaintEventArgs e) {
        this.animation.Draw(e.Graphics);
    }

When the animation needs to be repainted, it triggers a `Redraw` event. When this happens, `userControl1`
is invalidated. Moments later, when `userControl1` redraws itself, it tells the animation to draw itself.

This is such a common pattern that the framework provides a wrapper class to do this: `AnimationAdapter`.
Using that wrapper, the above code simply becomes::

    void RunAnimation() {
        AnimationAdapter adapter = new AnimationAdapter(this.userControl1);
        Animation animation = adapter.Animation;

        // Configure animation here

        animation.Start();
    }
