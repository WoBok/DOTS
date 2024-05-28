using Unity.Entities;

public partial class Lesson0203SystemGroup : ComponentSystemGroup { }

[UpdateInGroup(typeof(Lesson0203SystemGroup))]
public partial class RotateCubeSystemGroup : SceneSystemGroup
{
    protected override string AuthoringSceneName => "Lesson 02 03";
}