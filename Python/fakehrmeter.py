import io
import random
import time

while True:
    random_hr = random.randint(75, 120)
    
    #Store data in text file
    file = open('heartrate.txt', 'w')
    file.write(str(random_hr))
    file.close()
    time.sleep(1)