import time
import argparse
import asyncio
import logging
import io
from bleak import BleakClient, BleakScanner

selectedDevice = None
name = None
address = None

logger = logging.getLogger(__name__)

def notification_handler(sender, data):
    
    output_numbers = list(data)
    
    print(output_numbers[1])
    
    #Store data in text file
    file = open('heartrate.txt', 'w')
    file.write(str(output_numbers[1]))
    file.close()

async def main():

    uuid_hr_service = "0000180d-0000-1000-8000-00805f9b34fb"
    uuid_hr_characteristic = "00002a37-0000-1000-8000-00805f9b34fb"

    inputDeviceName = input('Enter device name: ')

    selectedDevice = await BleakScanner.find_device_by_name(inputDeviceName)

    name = selectedDevice.name
    address = selectedDevice.address

    print('Found device: ' + name + '\nAddress: ' + address)
    
    print('Connecting to device ' + name + ' with address ' + address + ' ...')
    
    async with BleakClient(address, winrt=dict(use_cached_services=True)) as client:

        if client.is_connected:
            
            print('Connected!')
        
            #services = client.services
            #for service in services:
            #    for char in service.characteristics:
            #        print("==================\n" + service.description + ' with ID: ' + str(service.handle) + ' and UUID ' + service.uuid)

            #Loop to receive heart rate notification
            while True:
                await client.start_notify(uuid_hr_characteristic, notification_handler)
                await asyncio.sleep(1.0)
                await client.stop_notify(uuid_hr_characteristic)
            


#Run main function through asyncio.run()
asyncio.run(main())
    



