# Training an Agent

## Setup

Make sure that you have installed conda and created the mlagents environment:

https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Installation.md

Pull the ML-Model branch and create a new branch for training to not interfere with others training data/model.

```
git pull test/ML-Model/stage-1
git checkout -b test/ML-Model/<--new name-->
```

Click on the slider gameobject in the ml_training_scene. The object's components should look like this:

<picture>
![alt text](https://github.com/chesterrc/ML-Breakout/tree/test/ML-Model/stage-1/Assets/Models/Images/slider_agent.png)
</picture>

## Running the model

To run the model, start the conda environment and navigate to the root directory of the project.

```
conda activate mlagents
cd path/to/<ml-breakout root folder>
```

Train the model with:

```
mlagents-learn path/to/config.yaml --run-id=<NameOfRun>
```

The model will run until the max_steps specified in the config. This was set to 5000.

## Config Overview
The config file is located at:
```
<ml-break root folder>/config/slider_config.yaml
```

```
behaviors: <--umbrella term to find mlagent behavior config with mlagents-learn-->
    SliderBehavior: <--behavior name-->
```

### General Parameters

| Settings            | Definition    |
|--------------------:|---------------|
|     trainer_type    |  Type of trainer. PPO, SAC or POCA. These are different types of reinforcement learning algorithms. This project is currently using PPO because the environment is simple ( doesn't contain multi-agent structure ).             |
|     max_steps    |  Number of iterations/steps/episodees to collect observations an do actions before ending the trining process.             |
|     time_horizon    |  Data of experiences collected before adding it to the experience buffer. The number should be large enough to capture all important behaviors within a sequence of an agent's actions. Typically between 32 - 2048             |
|     summary_freq    |  Data of experiences collected before generating and displaying training statistics.        |

### Hyperparameters

| Hyperparameters            | Definition    |
|---------------------------:|---------------|
|     batch_size             | Number of experiences i.e. how many samples the model processes before updating weights and biases. This number must be below the buffer_size. Continuous actions should be in the 1000s. Discrete actions are in the 10s. This project is currently using Continuous actions                |
|     buffer_size            | Number of agent observations, actions, adn rewards collected before updating the model              |
|     learning_rate          | Used to update exploration/curiosity of the model. Decrease if training is unstable.              |
|     beta                   | Controls randomness of an agent's actions during training.              |
|     epsilon                | Influeces how fast the agent predicts its next/best move.              |
|     lambd                  | How confident an agent is when using its current value estimate to predict the next move.              |
|     num_epoch              | Number of passes through the experiences (buffer_size). Decreasing this will ensure more stable updates at the cost of slower learning.               |
|     learning_rate_schedule | constant or linear. Constant means a fixed learning rate. Linear schedule means a decay overtime. The model is currently using linear because the idea should be that the agent's model converges to a solution overtime.              |
|     beta_schedule          | constant or linear. Constant means a steady randomness. Linear means a randomness that decays overtime due to the convergence of the agent's model to a solution. The model currently using constant.                |
|     epsilon_schedule       | constant or linear. Constant means a steady prediction rate. Linear means a prediction rate that decays overtime due to the convergence of the agent's model to a solution. The model currently using constant.                      |

### Network Settings

Configuration for the neural network being generated.

| Network Settings            | Definition    |
|----------------------------:|---------------|
| normalize            | Normalizes vector observations. Useful for complex continuous problems ( lots of different vectors from different objects that needs to be observed). Harmful for simpler discrete control problems. This model currently set this to true.    |
| hidden_units            | Number of units (nodes) within the hidden layers of the neural network. Typically in the range of 32 - 512. The model currently set this to 128   |
| num_layers            | Number of hidden layers after the observation input. Fewer layers are faster and more efficient to train. Large layers are used for complex problems. Typically in the range of 1 - 3. The model currently set this to 2.    |

### Reward Signals

Extrinsic signals are environment based rewards. Intrinsic signals (GAIL or curiosity) gives rewards based on exploration.

| Reward Signals            | Definition    |
|----------------------------:|---------------|
| Extrinsic --> gamma            | Discount factor for future rewards coming from the environment. How far in the future should the agent be thinking about possible rewards. This valus should be less than 1. |
| strength        | Factor by which to multiply the reward given by the environment. Typically set to 1 but depends on teh reward signal. The model currently has a range between 0.0 - 1.0 |

# More Information

https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Training-Configuration-File.md
