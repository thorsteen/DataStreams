from matplotlib import pyplot as plt
import numpy as np

ex7_arr = np.genfromtxt('Count-sketch_sorted_estimates.csv',delimiter=',',dtype = float)
ex8_arr3 = np.genfromtxt('Count-sketch_sorted_estimates_t3.csv',delimiter=',',dtype = float)
ex8_arr5 = np.genfromtxt('Count-sketch_sorted_estimates_t5.csv',delimiter=',',dtype = float)
ex8_arr10 = np.genfromtxt('Count-sketch_sorted_estimates_t10.csv',delimiter=',',dtype = float)


ex7_plot, ex7_ax = plt.subplots()
ex7_ax.spines["top"].set_visible(False)
ex7_ax.spines["right"].set_visible(False)
ex7_ax.spines["bottom"].set_visible(False)
ex7_ax.spines["left"].set_visible(False)
ex7_ax.plot(ex7_arr[:,0], ex7_arr[:,1], color='black',label = 'X')
ex7_ax.plot([0,100], [137296,137296], color='grey',label='S')
ex7_ax.legend(loc='upper left', frameon=False)
plt.xticks(np.arange(start=1, stop=101, step=9))
plt.ylim(np.amin(ex7_arr[:,1])-1000, np.amax(ex7_arr[:,1])+1000)
plt.xlim(1, len(ex7_arr[:,0]))
ex7_ax.set_title("Count-Sketch using t = 10 on a data stream with l = 13 and n = 100.000")
plt.ylabel("X and S sums")
plt.xlabel("X_1 ... X_{}".format(len(ex7_arr)))
ex7_plot.set_size_inches(11.69,4)
ex7_plot.savefig("ex7_plot.png")

ex8_plot, ex8_ax = plt.subplots()
ex8_ax.spines["top"].set_visible(False)
ex8_ax.spines["right"].set_visible(False)
ex8_ax.spines["bottom"].set_visible(False)
ex8_ax.spines["left"].set_visible(False)
ex8_ax.plot(ex8_arr3[:,0], ex8_arr3[:,1], color='blue', label='X with t=3')
ex8_ax.plot(ex8_arr5[:,0], ex8_arr5[:,1], color='yellow',label='X with t=5')
ex8_ax.plot(ex8_arr10[:,0], ex8_arr10[:,1], color='red',label='X with t=10')
ex8_ax.plot([0,100], [137296,137296], color='grey',label='S')
ex8_ax.legend(loc='upper left', frameon=False)
plt.xticks(np.arange(start=1, stop=101, step=9))
plt.ylim(np.amin(ex7_arr[:,1])-1000, np.amax(ex7_arr[:,1])+1000)
plt.xlim(1, len(ex7_arr[:,0]))
ex8_ax.set_title("Count-Sketch using t = 10 on a data stream with l = 13 and n = 100.000")
plt.ylabel("X and S sums")
plt.xlabel("X_1 ... X_{}".format(len(ex7_arr)))
ex8_plot.set_size_inches(11.69,4)
ex8_plot.savefig("ex8_plot.png")
