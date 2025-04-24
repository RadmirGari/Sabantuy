import topPicture from "../assets/top-template.jpg";

function Hero() {
    const scrollToForm = () => {
        const element = document.getElementById('subscribe-form');
        if (element) {
          element.scrollIntoView({ behavior: 'smooth' });
        }
      };


    return (
        <div className="relative w-full h-[500px]">
          <img
            src = {topPicture}
            alt="Hero Background"
            className="w-full h-full object-cover"
          />
          <div className="absolute inset-0 bg-black/50 flex flex-col items-center justify-center text-white text-center px-4">
            <h1 className="text-4xl font-bold mb-4">Welcome to Sabantuy</h1>
            <p className="mb-6 text-lg">Join us in celebrating community, culture, and connection.</p>
            <button
             onClick={scrollToForm}
             className="bg-red-400 text-black font-semibold px-6 py-3 rounded-full hover:bg-red-300 transition">
              Subscribe
            </button>
          </div>
        </div>
      );
}

export default Hero;