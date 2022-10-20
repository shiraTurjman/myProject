#include <stdio.h>
#include <stdlib.h>
#include <uchar.h>


bool isARPRespone(unsigned char *packetBuffer,  unsigned long packetSize, unsigned char fromIP[4], unsigned char toIP[4])
{
	// Check that the packet has sufficient size
	// ARP packets are 42 bytes
	if (packetSize != 42)
		return false;
	// Is it as ARP package?
	// Ethertype for ARP is 0x0806
	if (packetBuffer[12] != 0x08 ||
		packetBuffer[13] != 0x06)
		return false;
	// Does it specify the source IP address?
	unsigned char packetSourceIP[4];
	memcpy(packetSourceIP, packetBuffer + 28, 4);
	if (fromIP[0] != packetSourceIP[0] ||
		fromIP[1] != packetSourceIP[1] ||
		fromIP[2] != packetSourceIP[2] ||
		fromIP[3] != packetSourceIP[3])
		return false;
	// Does it specify the destination IP address?
	unsigned char packetDestIP[4];
	memcpy(packetDestIP, packetBuffer + 38, 4);
	if (toIP[0] != packetDestIP[0] ||
		toIP[1] != packetDestIP[1] ||
		toIP[2] != packetDestIP[2] ||
		toIP[3] != packetDestIP[3])
		return false;
	return true;
}