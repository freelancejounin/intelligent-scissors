I coded the three different scissors algorithms for this project.
After a grayscale images is loaded, you can select any number of points,
and then choose which algorithm you want to use to attempt to connect the
points in a circle using pixel gradients for edge weights.
There are three choices.
Straight scissors just draws a straight line from point to point
regardless of path.
Simple scissors uses a greedy algorithm to attempt to connect ecah point to the next.
Dijkstra's scissors uses Dijsktra's algorithm to find the cheapest path from one
point to the next.
Images for testing are included.