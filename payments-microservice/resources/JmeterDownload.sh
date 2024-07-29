#!/bin/bash

# Define variables
JMETER_VERSION=5.6.3
JMETER_URL=https://dlcdn.apache.org//jmeter/binaries/apache-jmeter-${JMETER_VERSION}.tgz
JMETER_DIR=/opt/apache-jmeter-${JMETER_VERSION}
JMETER_TGZ=apache-jmeter-${JMETER_VERSION}.tgz

# Download JMeter
echo "Downloading JMeter version ${JMETER_VERSION}..."
wget -q $JMETER_URL -O $JMETER_TGZ

# Move the downloaded file to /opt
echo "Moving JMeter to /opt..."
sudo mv $JMETER_TGZ /opt

# Change directory to /opt
cd /opt

# Extract the tarball
echo "Extracting JMeter..."
sudo tar -xzf $JMETER_TGZ

# Remove the tarball
echo "Removing the tarball..."
sudo rm $JMETER_TGZ

# Add JMeter to the PATH
echo "Adding JMeter to the PATH..."
echo "export PATH=\$PATH:${JMETER_DIR}/bin" | sudo tee -a /etc/profile

# Inform the user to source the profile or open a new terminal session
echo "JMeter installed successfully. Please source your profile or open a new terminal session to update the PATH."
