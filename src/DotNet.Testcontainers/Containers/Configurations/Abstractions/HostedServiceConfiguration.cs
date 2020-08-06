namespace DotNet.Testcontainers.Containers.Configurations.Abstractions
{
  using System.Collections.Generic;
  using DotNet.Testcontainers.Containers.OutputConsumers;
  using DotNet.Testcontainers.Containers.WaitStrategies;
  using JetBrains.Annotations;

  /// <summary>
  /// This class represents an extended Testcontainer configuration for modules. It is convenient for common configurations to provide a module with all necessary properties, without creating a new configuration again and again.
  /// </summary>
  public abstract class HostedServiceConfiguration
  {
    protected HostedServiceConfiguration(string image, int defaultPort)
      : this(image, defaultPort, defaultPort)
    {
    }

    protected HostedServiceConfiguration(string image, int defaultPort, int port)
    {
      this.Image = image;
      this.DefaultPort = defaultPort;
      this.Port = port;
      this.Environments = new Dictionary<string, string>();
      this.OutputConsumer = Consume.DoNotConsumeStdoutAndStderr();
      this.WaitStrategy = Wait.ForUnixContainer();
    }

    /// <summary>
    /// Gets the Docker image.
    /// </summary>
    [PublicAPI]
    public string Image { get; }

    /// <summary>
    /// Gets the container port.
    /// </summary>
    /// <remarks>
    /// Corresponds to the default port of the hosted service.
    /// </remarks>
    [PublicAPI]
    public int DefaultPort { get; }

    /// <summary>
    /// Gets the environment configuration.
    /// </summary>
    [PublicAPI]
    public IDictionary<string, string> Environments { get; }

    /// <summary>
    /// Gets the output consumer.
    /// </summary>
    /// <remarks>
    /// Uses <see cref="Consume.DoNotConsumeStdoutAndStderr" /> as default value.
    /// </remarks>
    [PublicAPI]
    public virtual IOutputConsumer OutputConsumer { get; }

    /// <summary>
    /// Gets the wait strategy.
    /// </summary>
    /// <remarks>
    /// Uses <see cref="Wait.ForUnixContainer" /> as default value.
    /// </remarks>
    [PublicAPI]
    public virtual IWaitForContainerOS WaitStrategy { get; }

    /// <summary>
    /// Gets or sets the host port.
    /// </summary>
    /// <remarks>
    /// Is bond to <see cref="DefaultPort" />.
    /// </remarks>
    [PublicAPI]
    public int Port { get; set; }

    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    [PublicAPI]
    public virtual string Username { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    [PublicAPI]
    public virtual string Password { get; set; }
  }
}
