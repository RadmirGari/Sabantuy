import Section from '../Components/Section';

const sampleItems = [
  { imageSrc: '/assets/test1.jpg', text: 'Text .....' },
  { imageSrc: '/assets/test2.jpg', text: 'More text .....' },
  { imageSrc: '/assets/test3.jpg', text: 'More text againasdasdsa.....' }
];

function AdminHome() {
    return(
        <div>
        <Section title="Activities" items={sampleItems} isAdmin />
        <Section title="History" items={sampleItems} isAdmin />
      </div>
    )
}

export default AdminHome;