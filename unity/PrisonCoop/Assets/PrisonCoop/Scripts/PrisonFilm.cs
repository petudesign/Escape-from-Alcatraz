using UnityEngine;
namespace PrisonCoop
{
    public sealed class PrisonFilm : MonoBehaviour
    {
        public Shader FilmShader;
        private Material film;
        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (FilmShader == null) { Graphics.Blit(source, destination); return; }
            if (film == null) film = new Material(FilmShader);
            Graphics.Blit(source, destination, film);
        }
        private void OnDestroy() { if (film != null) Destroy(film); }
    }
}
