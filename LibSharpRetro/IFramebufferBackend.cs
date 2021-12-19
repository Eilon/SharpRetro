namespace LibSharpRetro; 

public interface IFramebufferBackend : IGraphicsBackend {
	(int Width, int Height) Resolution { get; set; }
	Span<byte> Framebuffer { get; }

	void Flip();
}