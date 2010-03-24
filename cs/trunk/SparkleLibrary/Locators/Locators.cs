/*
 * Locators - A factory to create useful locators
 * 
 * Author: Phillip Piper
 * Date: 19/10/2009 1:02 AM
 *
 * Change log:
 * 2010-01-18   JPP  - Split out PointLocator.cs and RectangleFromCornersLocator.cs
 * 2009-10-19   JPP  - Initial version
 * 
 * To do:
 *
 * Copyright (C) 20009-2010 Phillip Piper
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 * If you wish to use this code in a closed source application, please contact phillip_piper@bigfoot.com.
 */

using System.Drawing;

namespace BrightIdeasSoftware
{
    /// <summary>
    /// Locators is a Factory that simplifies the creation of common locators.
    /// </summary>
    /// <remarks>Obviously, locators can still be created directly, but this class
    /// makes the creation of common locators easy.</remarks>
    public static class Locators
    {
        /// <summary>
        /// Create a PointLocator for the given fixed point
        /// </summary>
        public static IPointLocator At(int x, int y) {
            return new FixedPointLocator(new Point(x, y));
        }

        public static IPointLocator At(Point pt) {
            return new FixedPointLocator(pt);
        }

        /// <summary>
        /// Create a PointLocator that will align the given corner of a sprite
        /// at the corresponding corner of the animation.
        /// </summary>
        /// <remarks>For example,
        /// Locators.SpriteAligned(Corners.BottomRight) means the bottom right corner
        /// of the sprite will be placed at the bottom right corner of the animation.
        /// </remarks>
        /// <param name="corner">The corner to be aligned AND the corner at which it will be aligned</param>
        /// <returns>A point locator</returns>
        public static IPointLocator SpriteAligned(Corner corner) {
            return Locators.SpriteAligned(corner, corner, Point.Empty);
        }

        public static IPointLocator SpriteAligned(Corner corner, Point offset) {
            return Locators.SpriteAligned(corner, corner, offset);
        }

        public static IPointLocator SpriteAligned(Corner spriteCorner, Corner animationCorner) {
            return Locators.SpriteAligned(spriteCorner, animationCorner, Point.Empty);
        }

        public static IPointLocator SpriteAligned(Corner spriteCorner, Corner animationCorner, Point offset) {
            return new AlignedSpriteLocator(
                Locators.AnimationBoundsPoint(animationCorner),
                Locators.SpriteBoundsPoint(spriteCorner),
                offset);
        }

        public static IPointLocator SpriteAligned(Corner spriteCorner, float proportionX, float proportionY) {
            return Locators.SpriteAligned(spriteCorner, proportionX, proportionY, Point.Empty);
        }

        public static IPointLocator SpriteAligned(Corner spriteCorner, float proportionX, float proportionY, Point offset) {
            return new AlignedSpriteLocator(
                Locators.AnimationBoundsPoint(proportionX, proportionY),
                Locators.SpriteBoundsPoint(spriteCorner),
                offset);
        }

        /// <summary>
        /// Create a FixedRectangleLocator for the given fixed co-ordinates.
        /// </summary>
        public static IRectangleLocator At(int x, int y, int width, int height) {
            return new FixedRectangleLocator(new Rectangle(x, y, width, height));
        }

        public static IRectangleLocator At(Rectangle r) {
            return new FixedRectangleLocator(r);
        }

        public static IRectangleLocator AnimationBounds() {
            return new AnimationBoundsLocator();
        }

        public static IRectangleLocator AnimationBounds(int x, int y) {
            return new AnimationBoundsLocator(x, y);
        }

        public static IPointLocator SpriteBoundsPoint(Corner corner) {
            return new PointOnRectangleLocator(new SpriteBoundsLocator(), corner);
        }

        public static IPointLocator SpriteBoundsPoint(float proportionX, float proportionY) {
            return new PointOnRectangleLocator(new SpriteBoundsLocator(), proportionX, proportionY);
        }

        public static IPointLocator AnimationBoundsPoint(Corner corner) {
            return new PointOnRectangleLocator(new AnimationBoundsLocator(), corner);
        }

        public static IPointLocator AnimationBoundsPoint(Corner corner, int xOffset, int yOffset) {
            return new PointOnRectangleLocator(new AnimationBoundsLocator(), corner, new Point(xOffset, yOffset));
        }

        public static IPointLocator AnimationBoundsPoint(float proportionX, float proportionY) {
            return new PointOnRectangleLocator(new AnimationBoundsLocator(), proportionX, proportionY);
        }
    }
}
