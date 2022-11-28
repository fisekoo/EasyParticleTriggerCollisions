# :sparkles: Easy Particle Trigger Collisions (EPTC)
:sweat_drops: With Easy Particle Trigger Collisions, you can detect particle trigger collisions and tell unity what to do when particle collides, just like OnTriggerEnter functions!

![showCaseGitGif](https://user-images.githubusercontent.com/82342866/204170755-52d1e433-0a47-467a-98b0-0a01d7c38ec4.gif)

## :scroll: Description
EPTC is a script written by me for simplifying unity's particle system trigger collisions. By using this you can detect collisions just like detecting normal collisions! This currently only works for 2D.

#### ◤Features :bulb:
+ 👍 Easy to use.

<img src="https://user-images.githubusercontent.com/82342866/204167896-795147c9-15a3-47b1-b103-907cfc4baad1.gif" width="500" height="380,83"/>

+ :stars: Access the particle that collides with object.

<img src="https://user-images.githubusercontent.com/82342866/204168428-83efa730-49bb-468c-9b9d-c2d9ee4e9e2c.gif" width="350" height="301"/>

+ ✔️ Filter your collisions with Layer Mask.

![layerMaskingGif](https://user-images.githubusercontent.com/82342866/204168922-d55390cb-8d49-4075-b5dd-7453f88b99f6.gif)

## Initialization 💻
+ Add ParticleTriggerEvents folder to your project.

![image](https://user-images.githubusercontent.com/82342866/204169164-5bb64253-2cf6-4118-8b2b-2552ec87db00.png)

+ Create a Particle System object, add Particle Trigger Event Handler component to it and set layers.

![image](https://user-images.githubusercontent.com/82342866/204169283-b83c06fd-f59b-40cd-ae49-4e2c19919c8d.png)

+ Open a script where you want to detect collisions, implement Fisekoo namespace and IParticleTrigger interface.

![image](https://user-images.githubusercontent.com/82342866/204180681-56ef4ab5-c977-4d1f-ae3a-dfdf8d9fbcb1.png)


## Accessing and modifying collided particle
```csharp
var p = CollidedParticle; // getting particle properties.
p.startSize = 1.5f; // modifying it.
CollidedParticle = p; // applying.
```

## Variables
| Variable | Description |
| --- | --- |
| IParticleTrigger.CollidedParticle | Particle collided with object |
| Collides With | Layers which particles will interact. |
