<p align="center">
  <img width="256" heigth="256" src="assets/anti-ddos.png">
<h1 align="center">AntiDDoSProtection-Plugin</h1>
<p align="center">
  <strong>AntiDDoSProtection-Plugin</strong> is a plugin designed specifically for <strong>SCP: Secret Laboratory</strong> servers to protect them from attacks involving large volumes of network traffic. The plugin operates by analyzing incoming data packets and identifying suspicious or potentially malicious requests. It actively blocks packets that may indicate attempts at DDoS attacks or other types of network attacks, safeguarding the server from overloads and disruptions.
</p>
</p>
<p align="center">
  <img src="https://forthebadge.com/images/badges/built-with-love.svg" alt="appveyor-ci" />
  <img src="https://forthebadge.com/images/badges/made-with-c-sharp.svg" alt="appveyor-ci" />
</p>
</p>

## Features
- **Incoming packet analysis**: The plugin monitors and analyzes all incoming network packets to identify potential threats.
- **Suspicious packets blocking**: When suspicious or anomalous packets are detected, the plugin automatically blocks them, preventing possible attacks.
- **DDoS protection**: The plugin is specially optimized to protect against distributed denial of service attacks that can overload the server.

## Installation
1. Download the latest version of the plugin from [Releases](https://github.com/aksueikava/AntiDDoSProtection-Plugin/releases).
2. Place the plugin file in the `Plugins` folder of your SCP: SL server.
3. Restart the server to activate the plugin.

## Configuration
After the first run of the plugin, a configuration file is created. You can modify it as you wish:
```yml
AntiDDoSProtection:
# Is the plugin enabled?
  is_enabled: true
  # Are debug messages displayed?
  debug: false
  # Maximum number of packets allowed from a single IP per second.
  max_requests_per_second: 200
  # Maximum allowed size of a UDP packet in bytes.
  max_packet_size: 1024
  # Minimum allowed size of a UDP packet in bytes.
  min_packet_size: 1
  # List of IP addresses that are whitelisted and bypass all checks.
  whitelist_i_ps:
  - '127.0.0.1'
```