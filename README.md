# VRTrainingProject 🥽

Our Final project for our BSc.

![image](https://github.com/NattyZepko/VRTrainingProject/assets/50327680/5571a60a-3e09-4028-8b22-41fc650b0355)


A Unity-based AI learning project for detecting user gestures (motion) and instructing them in order to train.
The project uses MiVRy package for the AI learning Gesture Detection, and Unity UI for instructing the user.

---

## ❓ Q&A:


+ What lenguages did you use?
    - C#, standard with Unity, also very simple.

+ How does the program recognize your moves?
    - MiVRy package manages the Gesture Recognition with Machine-Learning. While we didn't write it ourselves, we do have an idea of how it works, with KNN algorithm. We feed it coordinates relative to the user's head as fixed points to learn, and the recognition is done by analyzing nearest-neighbor with Eucledean Distance.

+ Does it recognize any gesture? / Does it detect wrong gestures as false positive?
    - Hardly. Trial and error shows the correct gesture is often detected with over 50% certainty, so we set our program parameter to be 40%. It is, however, changable with a set hard-coded parameter in Unity, which any developer can change and set.

+ Which data did you train with? (aka who trained the machine the motions?)
    - We did. As a school project, we settled on our own motions as baseline the average users would have to replicate. In theory it works just fine, but in practice we see it doesn't fit some users. In a large enough project, with an actual budget we would have hired different professionals to performs the moves, and train the machine. It was not feasible in our time-frame.

+ Why even train in VR at all?
    - Access to VR is becoming much more massive and practical. In the near future it would probably become a common household item. That aside, training at home is becoming more common with treadmills, weights, etc. A VR could be one such tool in the future, with a program like ours to guide the users what to do, and how to train. This was a fun experiance at learning how to create a project in 3D, and tackling problems at scales we haven't encountered yet.

---

## 👥 Authors:

- [Natty Zepko](https://github.com/NattyZepko)
- [Yonatan Stekliar](https://github.com/YonatanStekliar)

---

## 🎬 Instructions video / Example:

Watch the video:

[![Watch the video](https://img.youtube.com/vi/SXXRw-dAFyY/default.jpg)]([https://youtu.be/nTQUwghvy5Q](https://www.youtube.com/watch?v=SXXRw-dAFyY&ab_channel=NattyZepko))

---

## 🎨 Image example:

![image](https://github.com/NattyZepko/VRTrainingProject/assets/50327680/0aa6cf46-6d79-4e49-a16a-46b2c2a05f7b)
