using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum EShader {None, Grayscale, FreezeDistortion};

public static class ShaderManager
{
    private static RenderStates grayscaleRenderStates;
    private static RenderStates doNothingRenderStates;
    private static RenderStates distortionRenderStates;

    private static Shader grayscaleShader;
    private static Shader doNothingShader;
    private static Shader distortionShader;

    public static void initialize()
    {
        grayscaleShader = new Shader(Constants.SHADERPATH + "IdleVertexShader.txt", Constants.SHADERPATH +  "GrayScaleShader.txt");
        grayscaleRenderStates = new RenderStates(grayscaleShader);
        grayscaleRenderStates.BlendMode = BlendMode.Alpha;

        doNothingShader = new Shader(Constants.SHADERPATH + "IdleVertexShader.txt", Constants.SHADERPATH + "IdleFragmentShader.txt");
        doNothingRenderStates = new RenderStates(doNothingShader);
        doNothingRenderStates.BlendMode = BlendMode.Alpha;

        distortionShader = new Shader(Constants.SHADERPATH + "DistortionVertexShader.txt", Constants.SHADERPATH + "GrayScaleShader.txt");
        distortionRenderStates = new RenderStates(distortionShader);
        distortionRenderStates.BlendMode = BlendMode.Alpha;
    }

    public static RenderStates getRenderState(EShader eShader)
    {
        switch (eShader)
        {
            case EShader.None:
                return doNothingRenderStates;

            case EShader.Grayscale:
                return grayscaleRenderStates;

            case EShader.FreezeDistortion:
                return distortionRenderStates;
    
        }

        return doNothingRenderStates;
    }
}

