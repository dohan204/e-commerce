import { Eye, Plus, Trash2, Edit, type LucideProps } from 'lucide-react';

const icons = {
  plus: Plus,
  eye: Eye,
  trash: Trash2,
  edit: Edit,
  // Thêm bao nhiêu tùy ý bạn vào đây
};

interface IconProps extends LucideProps {
  name: keyof typeof icons;
}

const Icons = ({ name, ...props }: IconProps) => {
  const LucideIcon = icons[name];
  if (!LucideIcon) return null; // Tránh crash nếu truyền tên sai
  return <LucideIcon {...props} />;
};

export default Icons;
