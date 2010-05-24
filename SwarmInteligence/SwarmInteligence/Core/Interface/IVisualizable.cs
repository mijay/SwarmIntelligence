using Utils.Drawing;

namespace SwarmInteligence
{
    /// <summary>
    /// Represents an object that can be drown.
    /// </summary>
    public interface IVisualizable
    {
        /// <summary>
        /// Draw an object on <paramref name="bitmap"/>.
        /// </summary>
        void Draw(FastBitmap bitmap);
    }
}