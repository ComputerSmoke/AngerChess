# AngerChess
Work-in-progress bot to play "Mad Chess".

Anger Chess is a bot designed to play "Mad Chess". It uses an object-oriented implementation of the game rules, and a convolutional neural network 
with a few deep layers to evaluate positions. A different pair of these neural networks will be applied to each matchup combination (A total of about 256 models).

The first of the models is for the matchup human-vs-human, and is trained on a database of regular chess games played online. Subsequent models will begin with
the same weights as human-vs-human (or another previously trained model), and tune their parameters by generating data through self-play.

Human-vs-human training code is available here: https://github.com/ComputerSmoke/AngerChessHuman
