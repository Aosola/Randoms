#ifndef WEIGHTED_GRAPH_H
#define WEIGHTED_GRAPH_H

#include <iostream>
#include <limits>
#include <queue>
#include "Exception.h"

class Weighted_graph {
	private:
		static const double INF;

		double **graph;
		int* degree_count;
		int count;
		int const size;

	public:
		Weighted_graph( int = 50 );
		~Weighted_graph();

		int degree( int ) const;
		int edge_count() const;
		double adjacent( int, int ) const;
		double minimum_spanning_tree( int ) const;
		bool is_connected() const;

		void insert( int, int, double );

	// Friends

	friend std::ostream &operator << ( std::ostream &, Weighted_graph const & );
};

const double Weighted_graph::INF = std::numeric_limits<double>::infinity();

Weighted_graph::Weighted_graph(int n):
count(0),
size(n)
{
	if (n < 0)
		throw illegal_argument();

	//instantiate and initialize adjacency matrix
	degree_count = new int[n];
	graph = new double *[n];
	double* temp = new double[n*n];
	//organize adjacency matrix into square matrix while setting degree of each node to 0
	for (int i = 0; i < n; i++)
	{
		graph[i] = &(temp[i*n]);
		degree_count[i] = 0;
	}
	//initialize adjacency matrix values to all INF except for where i = j 
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < n; j++)
		{
			graph[i][j] = INF;
		}
		graph[i][i] = 0;
	}
}

Weighted_graph::~Weighted_graph()
{
	//delete adjacency matrix then containing matrix
	if (size != 0)
		delete[] graph[0];
	delete[] graph;
	delete[] degree_count;
}

int Weighted_graph::degree(int i) const
{
	//returns degree of specified node
	if (i < 0 || i >= size)
		throw illegal_argument();
	return degree_count[i];
}

int Weighted_graph::edge_count() const
{
	//returns total number of edges
	return count;
}

double Weighted_graph::adjacent(int i, int j) const
{
	//throws exception if nodes are out of range
	if (i < 0 || j < 0 || i >= size || j >= size)
		throw illegal_argument();
	//return value of adjacency matrix
	return graph[i][j];
}

//Implementation of prim-jarnik algorithm to find mst with the use of array
//Runtime is O(size^2)
double Weighted_graph::minimum_spanning_tree(int m) const
{
	//throws exception if starting node is out of bound
	if (m < 0 || m >= size)
		throw illegal_argument();
	//initialize result, array to hold shortest cuts and array to record whether or not node is in visited set
	double result = 0;
	double *key = new double[size];
	bool *extracted = new bool[size];
	for (int i = 0; i < size; i++)
	{
		key[i] = INF;
		extracted[i] = false;
	}

	//set key of starting node to be 0 so that the algorithm starts operating with nodes adjacent to m
	key[m] = 0;
	//loops for a maximum of size-1 times as we need to make at most size-1 decision of which edge to take
	for (int i = 0; i < size - 1; i++)
	{
		//Extract minimum elements that has not been extracted
		int u = 0;
		//find first element that is not extracted
		for (int j = 0; j < size; j++)
		{
			if (!extracted[u])
				break;
			else
				u = j + 1;
		}
		//starting from that element, find minimum non-extracted element in key
		for (int j = u + 1; j < size; j++)
		{
			if (!extracted[j])
			{
				if (key[j] < key[u])
					u = j;
			}
		}
		//extract the minimum element
		extracted[u] = true;
		//if minimum element is INF, it means that there are no other nodes that are reachable from m so end algorithm
		if (key[u] == INF)
			break;

		//for each node that is adjacent (ie graph[u][j] is not INF) and not extracted, set key to edge weight if edge weight is smaller
		for (int j = 0; j < size; j++)
		{
			if (graph[u][j] != INF && !extracted[j] && graph[u][j] < key[j])
			{
				key[j] = graph[u][j];
			}
		}
	}
	
	//all edges used to traverse to reachable nodes are now in key, size of mst is sum of all these edges
	for (int i = 0; i < size; i++)
	{
		if (key[i] != INF)
			result = result + key[i];
	}
	//delete used arrays
	delete[] key;
	delete[] extracted;
	return result;
}


//Implementation of BFS to check if graph is connected
bool Weighted_graph::is_connected() const
{
	//Assume graph is not connected when there are no nodes
	if (size == 0)
		return false;

	//start BFS with node 0 and assume node count of BFS traversed tree starts with 1
	int connected_count = 1;
	bool *visited = new bool[size];
	for (int i = 0; i < size; i++)
	{
		visited[i] = false;
	}
	visited[0] = true;
	std::queue<int> Q;
	Q.push(0);
	while (!Q.empty())
	{
		int u = Q.front();
		for (int i = 0; i < size; i++)
		{
			if (graph[u][i] != INF && !visited[i])
			{
				visited[i] = true;
				//increment connected_count by 1 for each node visited
				connected_count++;
				Q.push(i);
			}
		}
		Q.pop();
	}

	delete[] visited;
	// graph is connected iff number of nodes visited is equivalent to number of nodes in graph (size)
	return connected_count == size;
}

void Weighted_graph::insert(int m, int n, double w)
{
	//throw exception if argument out of bound
	if (w < 0 || w == INF || m < 0 || n < 0 || m >= size || n >= size || m == n)
		throw illegal_argument();
	//w == 0 means to delete edge
	if (w == 0)
	{
		//decrement count, degree_count and set edge to INF if edge exists
		if (graph[m][n] != INF)
		{
			graph[m][n] = INF;
			graph[n][m] = INF;
			degree_count[m] = degree_count[m] - 1;
			degree_count[n] = degree_count[n] - 1;
			count--;
		}
		return;
	}
	//add edge and increment counts only if edge did not exist
	if (graph[m][n] == INF)
	{
		degree_count[m] = degree_count[m] + 1;
		degree_count[n] = degree_count[n] + 1;
		count++;
	}
	graph[m][n] = w;
	graph[n][m] = w;
}

std::ostream &operator << ( std::ostream &out, Weighted_graph const &graph ) {
	//print all elements in adjacency matrix in a beautiful manner
	for (int i = 0; i < graph.size; i++)
	{
		out << "i: " << i;
		for (int j = 0; j < graph.size; j++)
		{
			out << " j: " << j << " | " << graph.graph[i][j] << std::endl;
		}
	}
	return out;
}

#endif
