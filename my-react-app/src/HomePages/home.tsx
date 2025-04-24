import Hero from '../Components/Hero'
import Subscribe from '../Components/Subscribe';
import Section from '../Components/Section'

const sampleItems = [
    { imageSrc: '/assets/test1.jpg', text: 'Text .....' },
    { imageSrc: '/assets/test2.jpg', text: 'More text .....' },
    { imageSrc: '/assets/test3.jpg', text: 'More text againasdasdsa.....' }
  ];

function Home() {
    return(
        <div>
       
        <Hero />
        {/* Other sections like Features, Footer, etc */}
        <div>
        <Section title="Activities" items={sampleItems} />
        <Section title="History" items={sampleItems} />
        </div>

        <Subscribe />
      </div>
    )
}

export default Home;